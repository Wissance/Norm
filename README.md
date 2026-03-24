# 🚀 Norm

Ultra fast, safe (SQL Injection protected) multi threads/tasks (no locking SessionManager) .Net Database framework.

![Norm logo](img/logo/ai_3_sm.jpg)

## ✨ Features

`Norm` is stands for *Not an ORM* is a `C#` lib **for very HIGH Speed** `DB` data processing in `async` way with immediately operations and at background with Processing data with Batches. It **at least 10** times faster then `EF/EF.Core`. In Comparison to ORMs Norm has following disadvantages:

1. No object tracking
2. No Lazy loading properties
3. No Reflection usage

But it gives us the following:
1. Read speed is ultra fast (**500000 rows select from 1000000 rows in non-tuned (default) Mysql database ~10 ms**)

| Table size (rows)  | Rows to select   | Time, ms |
| ------------------ | ---------------- | -------- |
| 100k               | 10k              | 5        |
| 100k               | 10k in a middle  | 5        |
| 1M                 | 20k              | 7        |
| 1M                 | 100k             | 8        |
| 1M                 | 500k             | 9        |

2. Insert speed is fast (**10000 rows ~ 600 ms**, **100000 rows ~ 3000-5000 ms** on `i5` CPU for `MySql 8.0.23` with **default** settings)

| Rows to insert     | Time, ms |
| ------------------ | -------- |
| 100                | 13       |
| 1000               | 62       |
| 10000              | 549      |
| 100000             | 5111     |

3. Can work with DB in multiple threads unlike do all `ORMs`
4. Can synchronize data in background (for quite big operations, truly `async` behavior)
5. Can be used in `CQRS` approach because works in own thread and uses multiple tasks.

## 📦 Nuget

1. [Interface](https://www.nuget.org/packages/Wissance.nOrm/)
2. [Mysql](https://www.nuget.org/packages/Wissance.nOrm.MySql/)
3. [Postgres](https://www.nuget.org/packages/Wissance.nOrm.Postgres/)
4. [SqlServer](https://www.nuget.org/packages/Wissance.nOrm.SqlServer/)
5. [SqLite](https://www.nuget.org/packages/Wissance.nOrm.Sqlite/)

## 🔤 Changes

1. Version `0.1.0` `Read` and `Insert` (including `Bulk Insert`) operations were implemented with a `MySql` support only
2. Version `0.2.0` `Update` and `Bulk Update` operations support was added with a `MySql` support only
3. Version `0.5.0` `Delete` operation support was added with a `MySql` support only
4. Version `0.6.0` Some benchamarks tests were added with a `MySql` support only
5. Version `0.7.0` All functional tests were added to check all tests performed on a `MySql` support only
6. Version `0.8.0` `PostgreSQL` support was added
7. Version `0.9.0` `SqlServer` and `SQLite` support was added
8. Version `1.0.0` a `Nuget` packages were issued
10.Version `2.0.0` were added configurations and interface `WhereParameter` was used instead of Dictionary<string,string>
11.Version `2.0.0` minor patch fixes
12.Version `3.0.0` Used DbCommand instead of Raw SQL in BufferedDbRepository and added protection against SQL Injection
 
## 🤝 Contributors

<a href="https://github.com/Wissance/Norm/graphs/contributors">
  <img src="https://contrib.rocks/image?repo=Wissance/Norm" />
</a>
