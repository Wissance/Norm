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
    CONSTRAINT `fk_physical_value_id` FOREIGN KEY (`id`)
    REFERENCES `industrial_sensor_sys_mn`.`physical_values` (`id`)
    ON DELETE CASCADE 
    ON UPDATE CASCADE
);