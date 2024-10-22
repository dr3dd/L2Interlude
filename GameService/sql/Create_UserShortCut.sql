SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;
-- ----------------------------
-- Table structure for user_shortcut
-- ----------------------------
DROP TABLE IF EXISTS `user_shortcut`;
CREATE TABLE `user_shortcut`  (
  `char_id` int NOT NULL,
  `slot` int NOT NULL,
  `page` int NOT NULL,
  `type` int NOT NULL,
  `shortcut_id` int NOT NULL,
  `level` int NOT NULL,
  `class_index` int NOT NULL,
  PRIMARY KEY (`char_id`, `slot`, `page`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

SET FOREIGN_KEY_CHECKS = 1;
