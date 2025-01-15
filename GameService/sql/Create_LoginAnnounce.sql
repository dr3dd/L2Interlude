SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for login_announce
-- ----------------------------
DROP TABLE IF EXISTS `login_announce`;
CREATE TABLE `login_announce`  (
  `announce_id` int NOT NULL AUTO_INCREMENT,
  `announce_msg` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `interval` int NOT NULL,
  PRIMARY KEY (`announce_id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of login_announce
-- ----------------------------
INSERT INTO `login_announce` VALUES (1, 'NCORE Interlude Server based PTS data', 0);

SET FOREIGN_KEY_CHECKS = 1;
