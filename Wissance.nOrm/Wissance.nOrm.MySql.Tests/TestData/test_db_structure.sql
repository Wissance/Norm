CREATE TABLE `physical_values` (
    `id` INT NOT NULL AUTO_INCREMENT,
    `name` NVARCHAR(128) NOT NULL,
    `designation` NVARCHAR(32) NOT NULL,
    `description` NVARCHAR(256) NULL,
PRIMARY KEY (`id`));
