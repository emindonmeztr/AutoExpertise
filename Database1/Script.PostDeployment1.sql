/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/


CREATE TABLE [dbo].[technicalreport]
(
	 report_id  INT NOT NULL PRIMARY KEY,
	 process_id INT,
	 tarih date,
	 expert_notes VARCHAR(256),
	 on_aks_balans_farki INT,
	 arka_aks_balans_farki INT,
	 el_freni_balans_farki INT,
	 on_sol_amortisor INT,
	 on_sag_amortisor INT,
	 arka_sol_amortisor INT,
	 arka_sag_amortisor INT,
	 yanal_kayma INT,
	 )
