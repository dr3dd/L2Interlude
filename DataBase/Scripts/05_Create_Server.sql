CREATE TABLE `server` (
      `id` INT(11) NOT NULL,
      `name` VARCHAR(50) NOT NULL DEFAULT '',
      `ip` VARCHAR(15) NOT NULL DEFAULT '',
      `port` INT(11) NOT NULL DEFAULT 0
)
    ENGINE=InnoDB
;