SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;
-- ----------------------------
-- Table structure for user_skill
-- ----------------------------
DROP TABLE IF EXISTS `user_skill`;
CREATE TABLE `user_skill` (
      `char_id` INT(11) NOT NULL,
      `skill_id` INT(11) NOT NULL,
      `skill_level` TINYINT(4) NOT NULL DEFAULT 0,
      `to_end_time` INT(11) NOT NULL DEFAULT 0,
      PRIMARY KEY (`char_id`, `skill_id`)
)
COLLATE='utf8mb4_general_ci'
ENGINE=InnoDB
;

SET FOREIGN_KEY_CHECKS = 1;