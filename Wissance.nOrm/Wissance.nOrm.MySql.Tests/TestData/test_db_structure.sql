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
    CONSTRAINT `fk_physical_value_id` FOREIGN KEY (`physical_value_id`)
    REFERENCES `physical_values` (`id`)
    ON DELETE CASCADE 
    ON UPDATE CASCADE
);

CREATE TABLE `parameters` (
    `id` INT NOT NULL AUTO_INCREMENT,
    `name` NVARCHAR(256) NOT NULL,
    `aliases` NVARCHAR(512) NULL DEFAULT '',
    `description` NVARCHAR(512) NULL DEFAULT '',
    `measure_unit_id` INT NOT NULL,
    CONSTRAINT `fk_measure_unit_id` FOREIGN KEY (`measure_unit_id`)
    REFERENCES `measure_units` (`id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE
) 