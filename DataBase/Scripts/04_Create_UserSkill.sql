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