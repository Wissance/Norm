CREATE TABLE `physical_values` (
    `id` INT NOT NULL AUTO_INCREMENT,
    `name` NVARCHAR(128) NOT NULL,
    `designation` NVARCHAR(32) NOT NULL,
    `description` NVARCHAR(256) NULL,
    PRIMARY KEY (`id`)
);

CREATE TABLE `measure_units` (
   `id` INT NOT NULL AUTO_INCREMENT,
   `name` NVARCHAR(128) NOT NULL,
   `designation` NVARCHAR(32) NOT NULL,
   `description` NVARCHAR(256) NULL,
   `physical_value_id` INT NULL,
    PRIMARY KEY (`id`),
    CONSTRAINT `fk_measure_units_physical_value_id` FOREIGN KEY (`physical_value_id`)
    REFERENCES `physical_values` (`id`)
    ON DELETE CASCADE 
    ON UPDATE CASCADE
);

CREATE TABLE `parameters` (
    `id` INT NOT NULL AUTO_INCREMENT,
    `name` NVARCHAR(256) NOT NULL,
    `type` NVARCHAR(32) NOT NULL,
    `aliases` NVARCHAR(512) NULL DEFAULT '',
    `description` NVARCHAR(512) NULL DEFAULT '',
    `measure_unit_id` INT NOT NULL,
    `update_frequency` INT NOT NULL DEFAULT 0,
    PRIMARY KEY (`id`),
    CONSTRAINT `fk_parameters_measure_unit_id` FOREIGN KEY (`measure_unit_id`)
    REFERENCES `measure_units` (`id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE
);

-- for values inserting >= 300 s 
CREATE TABLE `parameters_values` (
    `id` BIGINT NOT NULL AUTO_INCREMENT,
    `value` TEXT NOT NULL,
    `time` TIMESTAMP NOT NULL,
    `parameter_id` INT NOT NULL,
    PRIMARY KEY (`id`),
    CONSTRAINT `fk_parameters_values_parameter_id` FOREIGN KEY (`parameter_id`)
    REFERENCES `parameters` (`id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE
);

-- for values inserting < 300 s 
CREATE TABLE `parameters_time_series_values` (
    `id` BIGINT NOT NULL AUTO_INCREMENT,
    `value` TEXT NOT NULL,
    `time` TIMESTAMP NOT NULL,
    `parameter_id` INT NOT NULL,
    PRIMARY KEY (`id`),
    CONSTRAINT `fk_parameters_time_series_values_parameter_id` FOREIGN KEY (`parameter_id`)
    REFERENCES `parameters` (`id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE
);

SET GLOBAL max_allowed_packet = 134217728;