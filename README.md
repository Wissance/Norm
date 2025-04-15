# Norm

## Features

Norm is stands for *Not an ORM* is a `C#` lib **for very HIGH Speed** `DB` data processing in `async` way at background with Processing data with Batches however due to Norm is NOT ORM it has following disadvantages:

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



