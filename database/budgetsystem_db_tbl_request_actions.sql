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
-- Table structure for table `tbl_request_actions`
--

DROP TABLE IF EXISTS `tbl_request_actions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_request_actions` (
  `requestID` int NOT NULL AUTO_INCREMENT,
  `requestedBy` varchar(50) DEFAULT NULL,
  `requestedTo` varchar(50) DEFAULT NULL,
  `status` varchar(50) DEFAULT NULL,
  `remark` text,
  `requestedAt` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `approvedAt` timestamp NULL DEFAULT NULL,
  `rejectedAt` timestamp NULL DEFAULT NULL,
  `revertedAt` timestamp NULL DEFAULT NULL,
  PRIMARY KEY (`requestID`)
) ENGINE=InnoDB AUTO_INCREMENT=114 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_request_actions`
--

LOCK TABLES `tbl_request_actions` WRITE;
/*!40000 ALTER TABLE `tbl_request_actions` DISABLE KEYS */;
INSERT INTO `tbl_request_actions` VALUES (96,'simran','AJIT PATIL','Pending','Budget request submitted','2025-03-22 06:59:15',NULL,NULL,NULL),(97,'simran','MANGESH MARBATE','Reverted','Budget request submitted','2025-03-22 07:06:42',NULL,NULL,'2025-03-22 01:50:10'),(98,'simran','MANGESH MARBATE','Reverted','Budget request submitted','2025-03-22 07:27:06',NULL,NULL,'2025-03-22 01:57:55'),(99,'simran','MANGESH MARBATE','Pending','Budget request submitted','2025-03-22 07:46:40',NULL,NULL,NULL),(100,'simran','MANGESH MARBATE','Approved','Budget request submitted','2025-03-22 07:50:09','2025-03-22 02:20:54',NULL,NULL),(101,'simran','MANGESH MARBATE','Pending','Budget request submitted','2025-03-22 08:27:04',NULL,NULL,NULL),(102,'simran','ABHISHEK THORAIT','Reverted','Budget request submitted','2025-03-25 05:30:48',NULL,NULL,'2025-03-25 00:17:28'),(103,'simran','ABHISHEK THORAIT','Reverted','Budget request submitted','2025-03-25 06:22:14',NULL,NULL,'2025-03-25 00:52:40'),(104,'simran','simran','Send with Corrections','Test remarks','2025-03-25 08:15:24',NULL,NULL,'2025-03-25 02:47:26'),(105,'simran','simran','Send with Corrections','Please review the corrections and resubmit','2025-03-25 11:18:19',NULL,NULL,'2025-03-25 05:48:53'),(106,'simran','simran','Send with Corrections','Please review the corrections and resubmit','2025-03-25 11:30:40',NULL,NULL,'2025-03-25 06:00:55'),(107,'simran','simran','SENT_CORR','Please review the corrections and resubmit','2025-03-25 11:49:49',NULL,NULL,'2025-03-25 06:20:08'),(108,'simran','ABHISHEK THORAIT','SENT_CORR','Please review the completed corrections','2025-03-26 09:04:33',NULL,NULL,'2025-03-26 03:35:21'),(109,'simran','AJAY BAPU MANKAR','Pending','Budget request submitted','2025-03-27 04:42:44',NULL,NULL,NULL),(110,'simran','ABHISHEK THORAIT','Approved','Please review the completed corrections','2025-03-27 05:31:02','2025-03-27 00:17:32',NULL,'2025-03-27 00:02:19'),(111,'simran','ABHISHEK THORAIT','Approved','Please review the completed corrections','2025-03-27 10:52:14','2025-03-27 05:33:22',NULL,'2025-03-27 05:30:16'),(112,'simran','ABHISHEK THORAIT','Approved','Budget request submitted','2025-03-27 11:09:46','2025-03-27 05:40:15',NULL,NULL),(113,'simran','ABHISHEK THORAIT','Pending','Budget request submitted','2025-03-27 11:12:12',NULL,NULL,NULL);
/*!40000 ALTER TABLE `tbl_request_actions` ENABLE KEYS */;
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
