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
-- Table structure for table `add_expense`
--

DROP TABLE IF EXISTS `add_expense`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `add_expense` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `ItemName` varchar(255) NOT NULL,
  `ApprovedBudget` decimal(18,2) DEFAULT '0.00',
  `ActualExpense` decimal(18,2) DEFAULT '0.00',
  `Month_ID` int NOT NULL,
  `Year_ID` int NOT NULL,
  `Remarks` varchar(255) DEFAULT NULL,
  `Timestamp` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`),
  KEY `Year_ID` (`Year_ID`),
  KEY `Month_ID` (`Month_ID`),
  CONSTRAINT `add_expense_ibfk_1` FOREIGN KEY (`Year_ID`) REFERENCES `tbl_financial_years` (`id`),
  CONSTRAINT `add_expense_ibfk_2` FOREIGN KEY (`Month_ID`) REFERENCES `months` (`Month_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `add_expense`
--

LOCK TABLES `add_expense` WRITE;
/*!40000 ALTER TABLE `add_expense` DISABLE KEYS */;
INSERT INTO `add_expense` VALUES (1,'Office Supplies',500.00,4508.00,1,1,'Stationery items purchased','2025-02-10 11:29:40'),(2,'Travel Expenses',1000.00,850.00,2,1,'Business trip to client','2025-02-10 11:29:40'),(3,'Marketing Campaign',3000.00,2800.00,3,1,'Summer promotion campaign','2025-02-10 11:29:40'),(4,'Software Licenses',2000.00,1900.00,4,1,'Renewal of software licenses','2025-02-10 11:29:40'),(5,'Employee Training',1200.00,1100.00,5,1,'Skill enhancement programs','2025-02-10 11:29:40'),(6,'Office Supplies',500.00,450.00,6,1,'Stationery items purchased for June','2025-02-10 11:29:40'),(7,'Travel Expenses',1200.00,1100.00,7,1,'Team travel for client meeting','2025-02-10 11:29:40'),(8,'Marketing Campaign',3500.00,3200.00,8,1,'Fall promotional campaign','2025-02-10 11:29:40'),(9,'Software Licenses',2200.00,2100.00,9,1,'Software renewals for the year','2025-02-10 11:29:40'),(10,'Employee Training',1500.00,1400.00,10,1,'Advanced workshops for employees','2025-02-10 11:29:40');
/*!40000 ALTER TABLE `add_expense` ENABLE KEYS */;
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
