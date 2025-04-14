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
-- Table structure for table `tbl_revert_remarks`
--

DROP TABLE IF EXISTS `tbl_revert_remarks`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_revert_remarks` (
  `revertID` int NOT NULL AUTO_INCREMENT,
  `requestID` varchar(50) DEFAULT NULL,
  `itemID` int DEFAULT NULL,
  `financialYearID` int DEFAULT NULL,
  `revertRemarks` text,
  `revertBy` varchar(50) DEFAULT NULL,
  `revertAt` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`revertID`),
  KEY `fk_itemID_revert` (`itemID`),
  KEY `fk_financialYearID_revert` (`financialYearID`),
  CONSTRAINT `fk_financialYearID_revert` FOREIGN KEY (`financialYearID`) REFERENCES `tbl_financial_years` (`id`),
  CONSTRAINT `fk_itemID_revert` FOREIGN KEY (`itemID`) REFERENCES `tbl_budgetitem` (`ItemID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=28 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_revert_remarks`
--

LOCK TABLES `tbl_revert_remarks` WRITE;
/*!40000 ALTER TABLE `tbl_revert_remarks` DISABLE KEYS */;
INSERT INTO `tbl_revert_remarks` VALUES (1,'6',NULL,NULL,'Budget request submitted',NULL,'2025-02-17 23:45:17'),(2,'8',NULL,NULL,'Budget request submitted','System','2025-02-18 00:20:35'),(3,'7',NULL,NULL,'Budget request submitted','System','2025-02-18 00:25:24'),(4,'9',NULL,NULL,'Budget request submitted','System','2025-02-18 00:34:28'),(5,'10',NULL,NULL,'Budget request submitted','System','2025-02-18 00:42:38'),(6,'11',NULL,NULL,'Budget request submitted','System','2025-02-18 02:17:09'),(7,'13',NULL,NULL,'Budget request submitted','System','2025-02-18 04:32:12'),(8,'15',NULL,NULL,'Budget request submitted','System','2025-02-18 05:32:20'),(9,'18',NULL,NULL,'Budget request submitted','System','2025-02-20 04:39:32'),(10,'19',NULL,NULL,'Budget request submitted','System','2025-02-20 04:49:52'),(11,'20',NULL,NULL,'Budget request submitted','System','2025-02-20 05:56:37'),(12,'26',NULL,NULL,'nsbnhjsw','System','2025-02-21 00:43:57'),(13,'27',NULL,NULL,'ndbwhjd','System','2025-02-21 02:08:31'),(14,'29',NULL,NULL,'dfbhj','System','2025-02-21 06:00:52'),(15,'32',NULL,NULL,'hjh','System','2025-02-23 23:54:12'),(16,'34',NULL,NULL,'No Remark','System','2025-02-24 00:15:46'),(17,'37',NULL,NULL,'nbvbvh','System','2025-02-24 01:00:59'),(18,'41',NULL,NULL,'jhdfewihgu','System','2025-03-02 22:01:14'),(19,'61',NULL,NULL,'bnvg','System','2025-03-05 23:22:21'),(20,'62',NULL,NULL,'nsbjkas','System','2025-03-05 23:34:57'),(21,'69',NULL,NULL,'dsnbfjs','System','2025-03-06 00:23:56'),(22,'86',NULL,NULL,'bvcgfcf','System','2025-03-20 05:19:07'),(23,'95',NULL,NULL,'gvgt','System','2025-03-21 03:21:06'),(24,'97',NULL,NULL,'ghfgf','System','2025-03-22 01:50:11'),(25,'103',NULL,NULL,'dsfsdf','System','2025-03-25 00:52:40'),(26,'104',NULL,NULL,'bhgvg','System','2025-03-25 02:47:26'),(27,'108',NULL,NULL,'mndfbhj','System','2025-03-26 03:35:21');
/*!40000 ALTER TABLE `tbl_revert_remarks` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-04-13 21:55:57
