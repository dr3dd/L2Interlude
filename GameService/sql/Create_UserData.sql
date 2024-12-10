SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;
-- ----------------------------
-- Table structure for user_data
-- ----------------------------
DROP TABLE IF EXISTS `user_data`;
CREATE TABLE `user_data` (
     `char_name` VARCHAR(50) NOT NULL,
     `char_id` INT(11) NOT NULL AUTO_INCREMENT,
     `account_name` VARCHAR(50) NOT NULL,
     `account_id` INT(11) NOT NULL,
     `gender` TINYINT(4) NOT NULL,
     `race` TINYINT(4) NOT NULL,
     `class` TINYINT(4) NOT NULL,
     `xloc` INT(11) NOT NULL,
     `yloc` INT(11) NOT NULL,
     `zloc` INT(11) NOT NULL,
     `isInVehicle` SMALLINT(6) NOT NULL DEFAULT 0,
     `cp` FLOAT NOT NULL,
     `hp` FLOAT NOT NULL,
     `mp` FLOAT NOT NULL,
     `sp` INT NOT NULL DEFAULT 0,
     `exp` INT(11) NOT NULL,
     `lev` TINYINT(4) NOT NULL,
     `pk` INT(11) NOT NULL DEFAULT 0,
     `duel` INT(11) NOT NULL DEFAULT 0,
     `st_underware` INT(11) NOT NULL DEFAULT 0,
     `st_right_ear` INT(11) NOT NULL DEFAULT 0,
     `st_left_ear` INT(11) NOT NULL DEFAULT 0,
     `st_neck` INT(11) NOT NULL DEFAULT 0,
     `st_right_finger` INT(11) NOT NULL DEFAULT 0,
     `st_left_finger` INT(11) NOT NULL DEFAULT 0,
     `st_head` INT(11) NOT NULL DEFAULT 0,
     `st_right_hand` INT(11) NOT NULL DEFAULT 0,
     `st_left_hand` INT(11) NOT NULL DEFAULT 0,
     `st_gloves` INT(11) NOT NULL DEFAULT 0,
     `st_chest` INT(11) NOT NULL DEFAULT 0,
     `st_legs` INT(11) NOT NULL DEFAULT 0,
     `st_feet` INT(11) NOT NULL DEFAULT 0,
     `st_back` INT(11) NOT NULL DEFAULT 0,
     `st_both_hand` INT(11) NOT NULL DEFAULT 0,
     `st_hair` INT(11) NOT NULL DEFAULT 0,
     `st_face` INT(11) NOT NULL DEFAULT 0,
     `st_hairall` INT(11) NOT NULL DEFAULT 0,
     `create_date` DATETIME NOT NULL DEFAULT (curdate()),
     `login` DATETIME NULL DEFAULT (curdate()),
     `logout` DATETIME NULL DEFAULT (curdate()),
     `quest_flag` binary(128) NULL DEFAULT 0x00,
     `nickname` VARCHAR(50) NULL DEFAULT NULL,
     `max_cp` INT(11) NOT NULL DEFAULT 0,
     `max_hp` INT(11) NOT NULL DEFAULT 0,
     `max_mp` INT(11) NOT NULL DEFAULT 0,
     `quest_memo` CHAR(32) NULL DEFAULT NULL,
     `face_index` INT(11) NOT NULL DEFAULT 0,
     `hair_shape_index` INT(11) NOT NULL DEFAULT 0,
     `hair_color_index` INT(11) NOT NULL DEFAULT 0,
     PRIMARY KEY (`char_id`),
     UNIQUE INDEX `char_name` (`char_name`),
     INDEX `account_id` (`account_id`)
)
COLLATE='utf8mb4_general_ci'
ENGINE=InnoDB
;

SET FOREIGN_KEY_CHECKS = 1;