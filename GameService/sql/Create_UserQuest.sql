SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for user_quest
-- ----------------------------
DROP TABLE IF EXISTS `user_quest`;
CREATE TABLE `user_quest`  (
  `char_id` int NOT NULL,
  `quest_no` int NOT NULL,
  `journal` int NOT NULL,
  `state1` int NOT NULL,
  `state2` int NOT NULL,
  `state3` int NOT NULL,
  `state4` int NOT NULL,
  `type` tinyint NOT NULL,
  PRIMARY KEY (`char_id`, `quest_no`) USING BTREE,
  UNIQUE INDEX `PK_user_queest`(`char_id` ASC, `quest_no` ASC) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

SET FOREIGN_KEY_CHECKS = 1;
