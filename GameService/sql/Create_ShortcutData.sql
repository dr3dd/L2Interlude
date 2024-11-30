SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;
-- ----------------------------
-- Table structure for shortcut_data
-- ----------------------------
DROP TABLE IF EXISTS `shortcut_data`;
CREATE TABLE `shortcut_data`  (
  `char_id` int NOT NULL,
  `slotnum` int NOT NULL,
  `shortcut_type` int NOT NULL,
  `shortcut_id` int NOT NULL,
  `shortcut_macro` int NOT NULL,
  `subjob_id` int NOT NULL,
  PRIMARY KEY (`char_id`, `slotnum`, `subjob_id`) USING BTREE,
  UNIQUE INDEX `PK_shortcut_data`(`char_id` ASC, `slotnum` ASC, `subjob_id` ASC) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

SET FOREIGN_KEY_CHECKS = 1;
