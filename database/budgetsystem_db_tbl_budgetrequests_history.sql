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
-- Table structure for table `tbl_budgetrequests_history`
--

DROP TABLE IF EXISTS `tbl_budgetrequests_history`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_budgetrequests_history` (
  `historyID` int NOT NULL AUTO_INCREMENT,
  `requestID` varchar(50) DEFAULT NULL,
  `itemID` int DEFAULT NULL,
  `financialYearID` int DEFAULT NULL,
  `Apr` decimal(10,2) DEFAULT '0.00',
  `May` decimal(10,2) DEFAULT '0.00',
  `Jun` decimal(10,2) DEFAULT '0.00',
  `Jul` decimal(10,2) DEFAULT '0.00',
  `Aug` decimal(10,2) DEFAULT '0.00',
  `Sep` decimal(10,2) DEFAULT '0.00',
  `Oct` decimal(10,2) DEFAULT '0.00',
  `Nov` decimal(10,2) DEFAULT '0.00',
  `Dec` decimal(10,2) DEFAULT '0.00',
  `Jan` decimal(10,2) DEFAULT '0.00',
  `Feb` decimal(10,2) DEFAULT '0.00',
  `Mar` decimal(10,2) DEFAULT '0.00',
  `totalAmount` decimal(10,2) DEFAULT '0.00',
  `remarks` text,
  `statusID` int DEFAULT '1',
  `action` varchar(10) NOT NULL,
  `changedAt` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `changedBy` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`historyID`),
  KEY `fk_itemID_history` (`itemID`),
  KEY `fk_statusID_history` (`statusID`),
  KEY `fk_financialYearID_history` (`financialYearID`),
  CONSTRAINT `fk_financialYearID_history` FOREIGN KEY (`financialYearID`) REFERENCES `tbl_financial_years` (`id`),
  CONSTRAINT `fk_itemID_history` FOREIGN KEY (`itemID`) REFERENCES `tbl_budgetitem` (`ItemID`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_statusID_history` FOREIGN KEY (`statusID`) REFERENCES `tbl_status` (`statusID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_budgetrequests_history`
--

LOCK TABLES `tbl_budgetrequests_history` WRITE;
/*!40000 ALTER TABLE `tbl_budgetrequests_history` DISABLE KEYS */;
/*!40000 ALTER TABLE `tbl_budgetrequests_history` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-04-13 21:55:56
