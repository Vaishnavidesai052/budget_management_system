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
-- Table structure for table `tbl_budgetdrafts`
--

DROP TABLE IF EXISTS `tbl_budgetdrafts`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_budgetdrafts` (
  `draftID` int NOT NULL AUTO_INCREMENT,
  `requestID` int DEFAULT NULL,
  `departmentID` int NOT NULL,
  `itemID` int NOT NULL,
  `financialYearID` int NOT NULL,
  `totalAmount` decimal(10,2) DEFAULT '0.00',
  `remarks` text,
  `status` enum('draft','submitted') DEFAULT 'draft',
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
  `createdAt` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `updatedAt` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `category_id` int NOT NULL,
  PRIMARY KEY (`draftID`),
  KEY `fk_department_key_` (`departmentID`),
  KEY `fk_item` (`itemID`),
  KEY `fk_financial_year` (`financialYearID`),
  KEY `fk_budgetdraft_category` (`category_id`),
  CONSTRAINT `fk_budgetdraft_category` FOREIGN KEY (`category_id`) REFERENCES `tbl_category` (`category_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_department_key_` FOREIGN KEY (`departmentID`) REFERENCES `department` (`DepartmentID`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_financial_year` FOREIGN KEY (`financialYearID`) REFERENCES `tbl_financial_years` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_item` FOREIGN KEY (`itemID`) REFERENCES `tbl_budgetitem` (`ItemID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=756 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_budgetdrafts`
--

LOCK TABLES `tbl_budgetdrafts` WRITE;
/*!40000 ALTER TABLE `tbl_budgetdrafts` DISABLE KEYS */;
INSERT INTO `tbl_budgetdrafts` VALUES (753,NULL,1,43,1,767.00,'','draft',767.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,'2025-03-27 11:13:49','2025-03-27 11:13:49',1),(754,NULL,1,45,1,66.00,'','draft',66.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,'2025-03-27 11:13:49','2025-03-27 11:13:49',1),(755,NULL,1,46,1,545.00,'','draft',0.00,545.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,'2025-03-27 11:13:49','2025-03-27 11:13:49',1);
/*!40000 ALTER TABLE `tbl_budgetdrafts` ENABLE KEYS */;
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
