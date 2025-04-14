-- MySQL dump 10.13  Distrib 8.0.40, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: budgetsystem_db
-- ------------------------------------------------------
-- Server version	8.0.40

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `tbl_item_remarks`
--

DROP TABLE IF EXISTS `tbl_item_remarks`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_item_remarks` (
  `id` int NOT NULL AUTO_INCREMENT,
  `requestID` int NOT NULL,
  `itemID` int NOT NULL,
  `remark` text,
  `remarkBy` varchar(255) DEFAULT NULL,
  `remarkAt` datetime DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_request` (`requestID`),
  KEY `fk_item_key` (`itemID`),
  CONSTRAINT `fk_item_key` FOREIGN KEY (`itemID`) REFERENCES `tbl_budgetitem` (`ItemID`) ON DELETE CASCADE,
  CONSTRAINT `fk_request` FOREIGN KEY (`requestID`) REFERENCES `tbl_request_actions` (`requestID`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=129 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_item_remarks`
--

LOCK TABLES `tbl_item_remarks` WRITE;
/*!40000 ALTER TABLE `tbl_item_remarks` DISABLE KEYS */;
INSERT INTO `tbl_item_remarks` VALUES (90,97,42,'hggg','System','2025-03-22 07:20:11'),(91,97,43,'ghfhtgf','System','2025-03-22 07:20:11'),(92,98,39,'kkhh','System','2025-03-22 07:27:55'),(93,98,42,'No HOD Remark','System','2025-03-22 07:27:55'),(94,98,43,'vvv','System','2025-03-22 07:27:55'),(95,98,45,'No HOD Remark','System','2025-03-22 07:27:55'),(96,98,46,'No HOD Remark','System','2025-03-22 07:27:55'),(97,98,47,'No HOD Remark','System','2025-03-22 07:27:55'),(98,98,48,'No HOD Remark','System','2025-03-22 07:27:55'),(99,102,43,'Ok ','System','2025-03-25 05:47:28'),(100,102,45,'change the month data','System','2025-03-25 05:47:28'),(101,102,46,'No HOD Remark','System','2025-03-25 05:47:28'),(102,103,43,'rfff','System','2025-03-25 06:22:40'),(103,103,45,'dfssf','System','2025-03-25 06:22:40'),(104,104,45,'hghhgfy hgfyt','System','2025-03-25 08:17:26'),(105,104,46,'bvcgfg hgfydtr','System','2025-03-25 08:17:26'),(106,105,45,'ghdfgsh','System','2025-03-25 11:18:53'),(107,105,46,'jdhdsh','System','2025-03-25 11:18:53'),(108,106,45,'JDHFGJHD','System','2025-03-25 11:30:55'),(109,106,46,'MDNBFDJ','System','2025-03-25 11:30:55'),(110,107,43,'dgfgdshg','System','2025-03-25 11:50:08'),(111,107,45,'nbdsfvhds','System','2025-03-25 11:50:08'),(112,108,46,'mfnb fdnm','System','2025-03-26 09:05:21'),(113,108,47,'ksjdfbjh','System','2025-03-26 09:05:21'),(114,108,43,'jdbjd','System','2025-03-26 09:05:21'),(115,110,39,'-','System','2025-03-27 05:32:19'),(116,110,42,'-','System','2025-03-27 05:32:19'),(117,110,43,'-','System','2025-03-27 05:32:19'),(118,110,45,'-','System','2025-03-27 05:32:19'),(119,110,46,'-','System','2025-03-27 05:32:19'),(120,110,47,'-','System','2025-03-27 05:32:19'),(121,110,48,'-','System','2025-03-27 05:32:19'),(122,110,49,'gggg','System','2025-03-27 05:32:19'),(123,110,50,'-','System','2025-03-27 05:32:19'),(124,110,51,'-','System','2025-03-27 05:32:19'),(125,111,45,'hshbdhgs','System','2025-03-27 11:00:16'),(126,111,46,'hgwfhyqwgd','System','2025-03-27 11:00:16'),(127,111,50,'-','System','2025-03-27 11:00:16'),(128,111,51,'-','System','2025-03-27 11:00:16');
/*!40000 ALTER TABLE `tbl_item_remarks` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-04-13 21:55:58
