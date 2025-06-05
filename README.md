# Norm

Ultra fast multi threads/tasks .Net Database framework.

![Norm logo](img/logo/ai_3_sm.jpg)

## Features

`Norm` is stands for *Not an ORM* is a `C#` lib **for very HIGH Speed** `DB` data processing in `async` way with immediately operations and at background with Processing data with Batches. It **at least 10** times faster then `EF/EF.Core`. In Comparison to ORMs Norm has following disadvantages:

1. No object tracking
2. No Lazy loading properties

But it gives us the following:
1. Read spead is fast (**10000 rows select from 100000 rows Mysql database ~100-200 ms**)

| Table size (rows)  | Rows to select   | Time, ms |
| ------------------ | ---------------- | -------- |
| 100k               | 10k              | 154      |
| 100k               | 10k in a middle  | 190      |
| 100k               | 100k             | 1114     |
| 1M                 | 100k             | 1135     |
| 1M                 | 500k             | 5624     |

2. Insert speed is fast (**10000 rows ~ 600 ms**, **100000 rows ~ 3000-5000 ms** on `i5` CPU for `MySql 8.0.23` with **default** settings)

| Rows to insert     | Time, ms |
| ------------------ | -------- |
| 100                | 13       |
| 1000               | 62       |
| 10000              | 549      |
| 100000             | 5111     |

3. Can work with DB in multiple threads unlike do all `ORMs`
4. Support `BULK` operations
5. Can synchronize data in background (for quite big operations, truly `async` behavior)
6. Can be used in `CQRS` approach because works in own thread and uses multiple tasks.
7. All modification operations (`Create`, `Update` and `Delete`) are using `DB Transactions`. 

## How to use

`Norm` works with persistant entities (table mapping or aggregate to multiple tables) via `Repository` interface `IDbRepository<T>` (T is a generic type of persistant object, i.e. `User`). `Norm` has following implementation of `IDbRepository<T>` interface:
1. `MySqlBufferedRepository<T>` for `MySql` DB server, it implements `BufferedDbRepository<T>`
2. `SqlServerBufferedRepository<T>` for `SqlServer` DB server, it implements `BufferedDbRepository<T>`
3. ` PostgresBufferedRepository<T>` for `Postgres` DB server, it implements `BufferedDbRepository<T>`
4. `SqLiteBufferedRepository<T>` for `SqLite` DB server, it implements `BufferedDbRepository<T>`

Consider how to use it on MySql Db:

1. Add the appropriate package; see the section below
2. Create a `IDbRepository<T>` instance as follows:
```csharp
    DbRepositorySettings dbRepositorySettings = new DbRepositorySettings()
    {
        BufferThreshold = 100,
        CommandTimeout = 120,
        BufferSynchronizationDelayTimeout = 100,
        ForceSynchronizationBufferDelay = 500
    };
    IDbRepository<PhysicalValueEntity> repo = new MySqlBufferedRepository<PhysicalValueEntity>(ConnectionString, dbRepositorySettings,
                                                                                               new PhysicalValueQueryBuilder(),
                                                                                               PhysicalValueFactory.Create, new NullLoggerFactory());
```

Contructor expect following params:
* DB connection string
* Repository settings:
  - `BufferThreshold` size of buffer (creainge or updainge entities) to sync
  - `Timeout` of executing command in seconds
  - `BufferSynchronizationDelayTimeout` time in ms between background synchronization iterations attempts
  - `ForceSynchronizationBufferDelay` timeout for sync if amount of items to create/update < BufferThreshold
* query builder that implements interface `IDbEntityQueryBuilder<TE>` (see example in tests)
* factory that builds Entity from array of column values (see also tests)
* logger factory

 3. Read Entities
    3.1 Without filtering
 ```csharp
    IList<PhysicalValueEntity> items = await repo.GetManyAsync(page, size, new List<WhereParameter>(), null);
 ```
    3.2 With filtering
 ```csharp
   IList<PhysicalValueEntity> items = await repo.GetManyAsync(page, size, new List<WhereParameter>()
   {
       new WhereParameter("id", null, false, WhereComparison.Greater, new List<object>(){lowerIdValue}, false),
       new WhereParameter("id", WhereJoinCondition.And, false, WhereComparison.Less, new List<object>(){upperIdValue}, false)
   }, null);
 ```
4. Create Entity
4.1 Single
```csharp
    PhysicalValueEntity entity = new PhysicalValueEntity()
    {
        Id = id,
        Name = "new phys value",
        Description = "new phys value",
        Designation = "NPV"
     };
     bool result = await repo.InsertAsync(entity, true);
```
The last insert method param is responsible for inserting into the Repository task in the background if false or immediately if true
4.2 Bulk
```csharp
    IList<PhysicalValueEntity> newPhysValues = new List<PhysicalValueEntity>()
    {
        new PhysicalValueEntity()
        {
            Id = 30,
            Name = "new phys value",
            Description = "new phys value",
            Designation = "NPV"
         },
         new PhysicalValueEntity()
         {
             Id = 31,
             Name = "new phys value2",
             Description = "new phys value2",
             Designation = "NPV2"
          },
          new PhysicalValueEntity()
          {
              Id = 32,
              Name = "new phys value3",
              Description = "new phys value3",
              Designation = "NPV3"
          }
     };
     int result = await repo.BulkInsertAsync(newPhysValues, true);
```
5. Update and bulk update are analogous to create methods:
6. Delete is quite a simple:
```csharp
    bool result = await repo.DeleteAsync(new List<WhereParameter>()
    {
        new WhereParameter("id", null, false, WhereComparison.Equal, new List<object>(){newPhysValue.Id})
    });
```

## Nuget

1. [Interface](https://www.nuget.org/packages/Wissance.nOrm/)
2. [Mysql](https://www.nuget.org/packages/Wissance.nOrm.MySql/)
3. [Postgres](https://www.nuget.org/packages/Wissance.nOrm.Postgres/)
4. [SqlServer](https://www.nuget.org/packages/Wissance.nOrm.SqlServer/)
5. [SqLite](https://www.nuget.org/packages/Wissance.nOrm.Sqlite/)

