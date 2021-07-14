CREATE TABLE `user_shortcut_data` (
    `char_id` INT(11) NOT NULL,
    `slotnum` INT(11) NOT NULL,
    `shortcut_type` INT(11) NOT NULL,
    `shortcut_id` INT(11) NOT NULL,
    `shortcut_macro` VARCHAR(256) NOT NULL DEFAULT '',
    PRIMARY KEY (`char_id`, `slotnum`)
)
COLLATE='utf8mb4_general_ci'
ENGINE=InnoDB