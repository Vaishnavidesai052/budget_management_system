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
-- Table structure for table `view_expense`
--

DROP TABLE IF EXISTS `view_expense`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `view_expense` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `ItemName` varchar(255) NOT NULL,
  `April` decimal(18,2) DEFAULT '0.00',
  `May` decimal(18,2) DEFAULT '0.00',
  `June` decimal(18,2) DEFAULT '0.00',
  `July` decimal(18,2) DEFAULT '0.00',
  `August` decimal(18,2) DEFAULT '0.00',
  `September` decimal(18,2) DEFAULT '0.00',
  `October` decimal(18,2) DEFAULT '0.00',
  `November` decimal(18,2) DEFAULT '0.00',
  `December` decimal(18,2) DEFAULT '0.00',
  `January` decimal(18,2) DEFAULT '0.00',
  `February` decimal(18,2) DEFAULT '0.00',
  `March` decimal(18,2) DEFAULT '0.00',
  `TotalAmount` decimal(18,2) DEFAULT '0.00',
  `Remarks` varchar(255) DEFAULT NULL,
  `CreatedAt` datetime DEFAULT CURRENT_TIMESTAMP,
  `Year_ID` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `Year_ID` (`Year_ID`),
  CONSTRAINT `view_expense_ibfk_1` FOREIGN KEY (`Year_ID`) REFERENCES `tbl_financial_years` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `view_expense`
--

LOCK TABLES `view_expense` WRITE;
/*!40000 ALTER TABLE `view_expense` DISABLE KEYS */;
INSERT INTO `view_expense` VALUES (1,'Employee Training',0.00,0.00,0.00,0.00,1100.00,0.00,0.00,0.00,0.00,1400.00,0.00,0.00,2500.00,'Advanced workshops for employees,Skill enhancement programs','2025-02-10 11:29:55',1),(2,'Marketing Campaign',0.00,0.00,2800.00,0.00,0.00,0.00,0.00,3200.00,0.00,0.00,0.00,0.00,6000.00,'Fall promotional campaign,Summer promotion campaign','2025-02-10 11:29:55',1),(3,'Office Supplies',450.00,0.00,0.00,0.00,0.00,450.00,0.00,0.00,0.00,0.00,0.00,0.00,900.00,'Stationery items purchased,Stationery items purchased for June','2025-02-10 11:29:55',1),(4,'Software Licenses',0.00,0.00,0.00,1900.00,0.00,0.00,0.00,0.00,2100.00,0.00,0.00,0.00,4000.00,'Renewal of software licenses,Software renewals for the year','2025-02-10 11:29:55',1),(5,'Travel Expenses',0.00,850.00,0.00,0.00,0.00,0.00,1100.00,0.00,0.00,0.00,0.00,0.00,1950.00,'Business trip to client,Team travel for client meeting','2025-02-10 11:29:55',1);
/*!40000 ALTER TABLE `view_expense` ENABLE KEYS */;
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
