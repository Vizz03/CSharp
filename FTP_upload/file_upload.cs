 if (FileUploadClientAccount.HasFile)
            {
                
                //FTP Server URL.
                string ftp = "ftp://ftp.dlptest.com/";

                //FTP Folder name. Leave blank if you want to upload to root folder.
                string ftpFolder = "";

                byte[] fileBytes = null;

                //Read the FileName and convert it to Byte array.
                string fileName = Path.GetFileName(FileUploadClientAccount.FileName);
                using (StreamReader fileStream = new StreamReader(FileUploadClientAccount.PostedFile.InputStream))
                {
                    fileBytes = Encoding.UTF8.GetBytes(fileStream.ReadToEnd());
                    fileStream.Close();
                }

                try
                {
                    //Create FTP Request.
                    FtpWebRequest requestClientAccount = (FtpWebRequest)WebRequest.Create(ftp + ftpFolder + fileName);
                    requestClientAccount.Method = WebRequestMethods.Ftp.UploadFile;

                    //Enter FTP Server credentials.
                    requestClientAccount.Credentials = new NetworkCredential("dlpuser@dlptest.com", "fLDScD4Ynth0p4OJ6bW6qCxjh");
                    requestClientAccount.ContentLength = fileBytes.Length;
                    requestClientAccount.UsePassive = true;
                    requestClientAccount.UseBinary = true;
                    requestClientAccount.ServicePoint.ConnectionLimit = fileBytes.Length;
                    requestClientAccount.EnableSsl = false;

                    using (Stream requestStream = requestClientAccount.GetRequestStream())
                    {
                        requestStream.Write(fileBytes, 0, fileBytes.Length);
                        requestStream.Close();
                    }

                    FtpWebResponse responseClientAccount = (FtpWebResponse)requestClientAccount.GetResponse();

                    lblClientAccountFeedback.Text += fileName + " uploaded.<br />";
                    responseClientAccount.Close();
                }
                catch (WebException ex)
                {
                    throw new Exception((ex.Response as FtpWebResponse).StatusDescription);
                }


            }
            else
            {
                lblClientAccountFeedback.Text = "No File Uploaded";
            }
        }
