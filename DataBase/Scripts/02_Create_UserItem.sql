CREATE TABLE `user_item` (
     `user_item_id` INT(11) NOT NULL AUTO_INCREMENT,
     `char_id` INT(11) NOT NULL,
     `item_id` INT(11) NOT NULL,
     `item_type` INT(11) NOT NULL,
     `amount` INT(11) NOT NULL,
     `enchant` INT(11) NOT NULL,
     `created_at` DATETIME NULL DEFAULT (curdate()),
     `updated_at` DATETIME NULL DEFAULT (curdate()),
     PRIMARY KEY (`user_item_id`),
     INDEX `char_id` (`char_id`),
     INDEX `item_id` (`item_id`),
     CONSTRAINT `FK_user_item_user_data` FOREIGN KEY (`char_id`) REFERENCES `user_data` (`char_id`) ON DELETE CASCADE
)
COLLATE='utf8mb4_general_ci'
ENGINE=InnoDB
;
