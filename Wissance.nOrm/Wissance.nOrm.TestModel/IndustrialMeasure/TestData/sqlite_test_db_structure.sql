CREATE TABLE physical_values (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    name VARCHAR(128) NOT NULL,
    designation VARCHAR(32) NOT NULL,
    description VARCHAR(256) NULL
);

CREATE TABLE measure_units (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    name VARCHAR(128) NOT NULL,
    designation VARCHAR(32) NOT NULL,
    description VARCHAR(256) NULL,
    physical_value_id INT NULL,
    CONSTRAINT fk_measure_units_physical_value_id FOREIGN KEY (physical_value_id)
        REFERENCES physical_values (id)
        ON DELETE CASCADE
        ON UPDATE CASCADE
);

CREATE TABLE parameters (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    name VARCHAR(256) NOT NULL,
    type VARCHAR(32) NOT NULL,
    aliases VARCHAR(512) NULL DEFAULT '',
    description VARCHAR(512) NULL DEFAULT '',
    measure_unit_id INT NOT NULL,
    update_frequency INT NOT NULL DEFAULT 0,
    CONSTRAINT fk_parameters_measure_unit_id FOREIGN KEY (measure_unit_id)
        REFERENCES measure_units (id)
        ON DELETE CASCADE
        ON UPDATE CASCADE
);

-- for values inserting >= 300 s 
CREATE TABLE parameters_values (
    id BIGINT PRIMARY KEY AUTOINCREMENT,
    value TEXT NOT NULL,
    time TIMESTAMP NOT NULL,
    parameter_id INT NOT NULL,
    CONSTRAINT fk_parameters_values_parameter_id FOREIGN KEY (parameter_id)
        REFERENCES parameters (id)
        ON DELETE CASCADE
        ON UPDATE CASCADE
);

-- for values inserting < 300 s 
CREATE TABLE parameters_time_series_values (
    id BIGINT PRIMARY KEY AUTOINCREMENT,
    value TEXT NOT NULL,
    time TIMESTAMP NOT NULL,
    parameter_id INT NOT NULL,
    CONSTRAINT fk_parameters_time_series_values_parameter_id FOREIGN KEY (parameter_id)
        REFERENCES parameters (id)
        ON DELETE CASCADE
        ON UPDATE CASCADE
);