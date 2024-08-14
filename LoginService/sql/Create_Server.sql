SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;
-- ----------------------------
-- Table structure for server
-- ----------------------------
DROP TABLE IF EXISTS `server`;
CREATE TABLE `server` (
      `id` INT(11) NOT NULL,
      `name` VARCHAR(50) NOT NULL DEFAULT '',
      `ip` VARCHAR(15) NOT NULL DEFAULT '',
      `port` INT(11) NOT NULL DEFAULT 0
)
    ENGINE=InnoDB
;

SET FOREIGN_KEY_CHECKS = 1;