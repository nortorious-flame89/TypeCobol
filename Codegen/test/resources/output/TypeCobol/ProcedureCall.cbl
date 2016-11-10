﻿      * 10 CodeElements errors
      * "1"@(37:12>39:50): [27:1] Syntax error : Symbol ValidateDatFormatt is not referenced
      * "1"@(47:12>49:50): [27:1] Syntax error : Function ValidateDateFormat expected parameter 3 of type BOOL (actual: Alphanumeric)
      * "1"@(47:12>49:50): [27:1] Syntax error : Function ValidateDateFormat expected parameter 3 of max length 1 (actual: 8)
      * "1"@(47:12>49:50): [27:1] Syntax error : Function ValidateDateFormat is missing parameter 4 of type Alphanumeric
      * "1"@(53:12>55:46): [27:1] Syntax error : Function ValidateDateFormat expected parameter 1 of type DATE (actual: Alphanumeric)
      * "1"@(53:12>55:46): [27:1] Syntax error : Function ValidateDateFormat expected parameter 1 of max length 1 (actual: 8)
      * "1"@(53:12>55:46): [27:1] Syntax error : Function ValidateDateFormat expected parameter 2 of type Alphanumeric (actual: DATE)
      * "1"@(53:12>55:46): [27:1] Syntax error : Function ValidateDateFormat expected parameter 3 of type BOOL (actual: Alphanumeric)
      * "1"@(53:12>55:46): [27:1] Syntax error : Function ValidateDateFormat expected parameter 3 of max length 1 (actual: 8)
      * "1"@(53:12>55:46): [27:1] Syntax error : Function ValidateDateFormat expected parameter 4 of type Alphanumeric (actual: BOOL)
       IDENTIFICATION DIVISION.
       PROGRAM-ID. ProcedureCall.

       DATA DIVISION.
       LOCAL-STORAGE SECTION.

      *01  somedate     TYPE Date.                                            
       01 somedate.                                                           
           02 YYYY PIC 9(4).                                                  
           02 MM PIC 9(2).                                                    
           02 DD PIC 9(2).                                                    
       01  someformat   PIC X(08).
      *01  flag         TYPE Bool.                                            
       01  flag-value PIC X VALUE LOW-VALUE.                                  
           88  flag       VALUE 'T'.                                          
           88  flag-false VALUE 'F'.                                          
       01  realformat   PIC X(08).

       PROCEDURE DIVISION.
       
      *DECLARE PROCEDURE ValidateDateFormat PRIVATE                           
      *    INPUT mydate        TYPE Date                                      
      *          format        PIC X(08)                                      
      *   OUTPUT okay          TYPE Bool                                      
      *          actual-format PIC X(08).                                     
      *  .                                                                    

       TRAITEMENT.
      * __________________________________________________
      * OK : proper parameter list (TCRFUN_CALL_PARAMETER_ORDER)
      *    CALL ValidateDateFormat                                            
      *             INPUT      somedate someformat                            
      *             OUTPUT     flag     realformat                            
       CALL ValidateDateFormat                                                
           USING  somedate someformat flag realformat                         
       
      *    CALL ValidateDateFormat                                            
      *             INPUT      somedate someformat                            
      *             OUTPUT     flag     realformat                            
       CALL ValidateDateFormat                                                
           USING  somedate someformat flag realformat                         
      * __________________________________________________
      * KO : procedure doesn't exist
      *    CALL ValidateDatFormatt                                            
      *             INPUT      somedate someformat                            
      *             OUTPUT              realformat                            
       CALL ValidateDatFormatt                                                
           USING  somedate someformat realformat                              
      * __________________________________________________
      * OK : parameter number for a procedure
      *      however, this is parsed as a standard COBOL call
           CALL ValidateDateFormat END-CALL
      * __________________________________________________
      * KO : wrong parameter number (TCRFUN_MATCH_PARAMETERS_NUMBER)
      *    CALL ValidateDateFormat                                            
      *             INPUT      somedate someformat                            
      *             OUTPUT              realformat                            
       CALL ValidateDateFormat                                                
           USING  somedate someformat realformat                              
      * __________________________________________________
      * KO : wrong parameter order (TCRFUN_MATCH_PARAMETERS_TYPE)
      *    CALL ValidateDateFormat                                            
      *             INPUT      someformat somedate                            
      *             OUTPUT     realformat flag                                
       CALL ValidateDateFormat                                                
           USING  someformat somedate realformat flag                         
           .

       END PROGRAM ProcedureCall.
      *_________________________________________________________________      
       IDENTIFICATION DIVISION.                                               
       PROGRAM-ID. ValidateDateFormat-01.                                     
       DATA DIVISION.                                                         
       LINKAGE SECTION.                                                       
       01 mydate.                                                             
           02 YYYY PIC 9(4).                                                  
           02 MM PIC 9(2).                                                    
           02 DD PIC 9(2).                                                    
       01 format PIC X(08).                                                   
       01 okay-value PIC X     VALUE LOW-VALUE.                               
           88 okay       VALUE 'T'.                                           
           88 okay-false VALUE 'F'.                                           
       01 actual-format PIC X(08).                                            
       PROCEDURE DIVISION                                                     
             USING BY REFERENCE mydate                                        
                   BY REFERENCE format                                        
                   BY REFERENCE okay                                          
                   BY REFERENCE actual-format                                 
           .                                                                  
           CONTINUE.
       END PROGRAM ValidateDateFormat-01.                                     