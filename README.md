# Norm

Norm is stands for *Not an ORM* is a `C#` lib **for very HIGH Speed** `DB` data processing in `async` way at background with Processing data with Batches however due to Norm is NOT ORM it has following disadvantages:

1. No object tracking
2. No Lazy loading properties

But it gives us the following:
1. Read spead is fast 
2. Insert speed is fast (**10000 rows ~ 600 ms**, **100000 rows ~ 3000-5000 ms** on `i5` CPU for `MySql 8.0.23` with **default** settings)
3. Can work with DB in multiple threads unlike many other `ORMs`

