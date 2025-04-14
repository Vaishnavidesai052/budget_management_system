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
-- Table structure for table `request_history`
--

DROP TABLE IF EXISTS `request_history`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `request_history` (
  `history_id` int NOT NULL AUTO_INCREMENT,
  `request_id` int NOT NULL,
  `action` varchar(100) NOT NULL,
  `performed_by` varchar(50) NOT NULL,
  `performed_to` varchar(50) DEFAULT NULL,
  `remarks` text,
  `action_date` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`history_id`),
  KEY `request_id` (`request_id`),
  CONSTRAINT `request_history_ibfk_1` FOREIGN KEY (`request_id`) REFERENCES `tbl_request_actions` (`requestID`)
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `request_history`
--

LOCK TABLES `request_history` WRITE;
/*!40000 ALTER TABLE `request_history` DISABLE KEYS */;
INSERT INTO `request_history` VALUES (1,104,'Submitted','simran','ABHISHEK THORAIT','Initial submission','2025-03-25 08:15:24'),(3,105,'Submitted','simran','ABHISHEK THORAIT','Initial submission','2025-03-25 11:18:19'),(4,105,'Send with Corrections','ABHISHEK THORAIT','simran','Please review the corrections and resubmit','2025-03-25 11:19:36'),(5,106,'Submitted','simran','ABHISHEK THORAIT','Initial submission','2025-03-25 11:30:40'),(6,106,'Send with Corrections','ABHISHEK THORAIT','simran','Please review the corrections and resubmit','2025-03-25 11:33:14'),(7,107,'Submitted','simran','ABHISHEK THORAIT','Initial submission','2025-03-25 11:49:49'),(8,107,'SENT_CORR','ABHISHEK THORAIT','simran','Please review the corrections and resubmit','2025-03-25 11:50:33'),(9,108,'Submitted','simran','ABHISHEK THORAIT','Initial submission','2025-03-26 09:04:33'),(10,108,'SENT_CORR','ABHISHEK THORAIT','ABHISHEK THORAIT','Please review the completed corrections','2025-03-26 09:09:15'),(11,109,'Submitted','simran','AJAY BAPU MANKAR','Initial submission','2025-03-27 04:42:44'),(12,110,'Submitted','simran','ABHISHEK THORAIT','Initial submission','2025-03-27 05:31:02'),(13,110,'SENT_CORR','ABHISHEK THORAIT','ABHISHEK THORAIT','Please review the completed corrections','2025-03-27 05:32:48'),(14,111,'Submitted','simran','ABHISHEK THORAIT','Initial submission','2025-03-27 10:52:14'),(15,111,'SENT_CORR','ABHISHEK THORAIT','ABHISHEK THORAIT','Please review the completed corrections','2025-03-27 11:00:39'),(16,112,'Submitted','simran','ABHISHEK THORAIT','Initial submission','2025-03-27 11:09:46'),(17,113,'Submitted','simran','ABHISHEK THORAIT','Initial submission','2025-03-27 11:12:12');
/*!40000 ALTER TABLE `request_history` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-04-13 21:55:59
