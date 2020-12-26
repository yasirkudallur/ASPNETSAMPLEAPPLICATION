using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EmiratesId.AE.PublicData;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json.Linq;

public partial class frmTestDetailsTemp : System.Web.UI.Page
{

    clsBllVisitorInfo objBll = new clsBllVisitorInfo();
    string strReturnData = "";
    DataSet objDs;
    String strMail_Body = "";

    [WebMethod]
    public static string[] GetCustomers(string prefix)
    {
        List<string> customers = new List<string>();
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = System.Configuration.ConfigurationManager.AppSettings["dbcon"].ToString();
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select company_name_en, company_id from COMPANY where company_name_en like @SearchText + '%'";
                cmd.Parameters.AddWithValue("@SearchText", prefix);
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}-{1}", sdr["company_name_en"], sdr["company_id"]));
                    }
                }
                conn.Close();
            }
        }
        return customers.ToArray();
    }
    private string checkPendingTest()//BY IDN OR MOBILE NUMBER AND DATE
    {
        try
        {
            strReturnData = objBll.checkPendingTest(idn.Text);
            return strReturnData;
        }
        catch (Exception ex)
        {
            return "";
        }
    }
    public void loadTestCompleteDetails(int iVisitorId)
    {
        try
        {
            objDs = objBll.loadTestCompleteDetails(iVisitorId);

            if (objDs.Tables[0].Rows.Count > 0)
            {
                this.idn.Text = objDs.Tables[0].Rows[0]["IDN"].ToString();
                this.cardHolderNameAr.Text = objDs.Tables[0].Rows[0]["NAME_AR"].ToString();
                this.cardHolderNameEn.Text = objDs.Tables[0].Rows[0]["NAME_EN"].ToString();
                if (objDs.Tables[0].Rows[0]["GENDER"].ToString() == "1")
                {
                    this.radGender.Items[0].Selected = true;
                }
                else
                {
                    this.radGender.Items[1].Selected = true;
                }
                this.txtDob.Text = objDs.Tables[0].Rows[0]["DOB"].ToString();
                this.drpNationality.SelectedIndex = this.drpNationality.Items.IndexOf(this.drpNationality.Items.FindByValue(objDs.Tables[0].Rows[0]["NATIONALITY"].ToString()));

                this.txtNationality.Text = objDs.Tables[0].Rows[0]["NATIONALITY_NAME_AR"].ToString();
                this.txtNationality.ToolTip = objDs.Tables[0].Rows[0]["NATIONALITY_CODE"].ToString();

                this.txtPassportNumber.Text = objDs.Tables[0].Rows[0]["PASSPORT_NUMBER"].ToString();
                this.visitor_mobile.Text = objDs.Tables[0].Rows[0]["MOB_NUMBER"].ToString();
                this.txtEmail.Text = objDs.Tables[0].Rows[0]["EMAIL"].ToString();
                this.drpCategory.SelectedIndex = this.drpCategory.Items.IndexOf(this.drpCategory.Items.FindByValue(objDs.Tables[0].Rows[0]["CATEGORY_ID"].ToString()));
                this.txtJobInfo.Text = objDs.Tables[0].Rows[0]["JOB_INFO"].ToString();
                this.txtAddress.Text = objDs.Tables[0].Rows[0]["ADDRESS"].ToString();


                if (objDs.Tables[0].Rows[0]["IMAGE_BYTE"].ToString().Trim() != "")
                {
                    if (objDs.Tables[0].Rows[0]["IMAGE_BYTE"].ToString().Trim().Length > 1500)
                    {
                        PhotoBase64.Src = "data:image/jpeg;base64," + objDs.Tables[0].Rows[0]["IMAGE_BYTE"].ToString().Trim();
                    }
                }
                else
                {
                    PhotoBase64.Src= "~/Images/user-alt.png";
                }

            }


            if (objDs.Tables[1].Rows.Count > 0)
            {
                if (objDs.Tables[1].Rows[0]["TEST_ONE_EYE"].ToString().ToUpper() == "TRUE")
                {
                    this.chkTestOneEye.Checked = true;
                }

                if (objDs.Tables[1].Rows[0]["TEST_ONE_HEIGHT"].ToString().ToUpper() == "TRUE")
                {
                    this.chkTestOneHeight.Checked = true;
                }

                if (objDs.Tables[1].Rows[0]["TEST_ONE_WEIGHT"].ToString().ToUpper() == "TRUE")
                {
                    this.chkTestOneWeight.Checked = true;
                }

                if (objDs.Tables[1].Rows[0]["TEST_TWO_BLOOD"].ToString().ToUpper() == "TRUE")
                {
                    this.chkTestTwoBlood.Checked = true;
                }

                if (objDs.Tables[1].Rows[0]["TEST_TWO_URINE"].ToString().ToUpper() == "TRUE")
                {
                    this.chkTestTwoUrine.Checked = true;
                }

                if (objDs.Tables[1].Rows[0]["TEST_THREE_XRAY"].ToString().ToUpper() == "TRUE")
                {
                    this.chkTestThreeXray.Checked = true;
                }

                if (objDs.Tables[1].Rows[0]["TEST_FOUR_EYE"].ToString().ToUpper() == "TRUE")
                {
                    this.chkTestFourEye.Checked = true;
                }

                if (objDs.Tables[1].Rows[0]["TEST_FOUR_BONE"].ToString().ToUpper() == "TRUE")
                {
                    this.chkTestFourBone.Checked = true;
                }

                if (objDs.Tables[1].Rows[0]["TEST_FOUR_SKIN"].ToString().ToUpper() == "TRUE")
                {
                    this.chkTestFourSkin.Checked = true;
                }

                if (objDs.Tables[1].Rows[0]["TEST_FOUR_PSYCH"].ToString().ToUpper() == "TRUE")
                {
                    this.chkTestFourPsych.Checked = true;
                }

                if (objDs.Tables[1].Rows[0]["TEST_FOUR_SURGERY"].ToString().ToUpper() == "TRUE")
                {
                    this.chkTestFourSurgery.Checked = true;
                }

                if (objDs.Tables[1].Rows[0]["TEST_FOUR_GP"].ToString().ToUpper() == "TRUE")
                {
                    this.chkTestFourGp.Checked = true;
                }


                if (objDs.Tables[1].Rows[0]["RESULT_STATUS"].ToString().ToUpper() == "1")
                {
                    this.radFitnessSelection.Items[0].Selected = true;
                }
                else if (objDs.Tables[1].Rows[0]["RESULT_STATUS"].ToString().ToUpper() == "0")
                {
                    this.radFitnessSelection.Items[1].Selected = true;
                }

            }
        }
        catch (Exception ex)
        {
        }
    }
    private void saveVisitor(int opFlag)//opFlag = 1 then pre registration opflag = 2 then registration
    {
        System.Threading.Thread.Sleep(750);

           this.tblMessage.Visible = false;
            try
            {
                objBll.sIdn = this.idn.Text;
                objBll.sNameAr = this.cardHolderNameAr.Text;
                objBll.sNameEn = this.cardHolderNameEn.Text;
                if (radGender.Items[0].Selected)
                {
                    objBll.sGender = "M";
                }
                else
                {
                    objBll.sGender = "F";
                }
                try
                {
                    objBll.dtDob = DateTime.Parse(txtDob.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-AU").DateTimeFormat);
                }
                catch (Exception ex)
                {

                }
                //objBll.sCardNum = this.txtCardNumber.Text;
                //try
                //{
                //    objBll.dtIssueDate = DateTime.Parse(txtCardIssueDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-AU").DateTimeFormat);
                //}
                //catch (Exception ex)
                //{

                //}
                //try
                //{
                //    objBll.dtExpiryDate = DateTime.Parse(txtCardExpiryDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-AU").DateTimeFormat);
                //}
                //catch (Exception ex)
                //{

                //}
                objBll.sNationality = drpNationality.SelectedItem.Value;

                objBll.sMobileVisitor = this.visitor_mobile.Text;
               
                objBll.iCategoryId = Convert.ToInt32(drpCategory.SelectedItem.Value);
                objBll.iTrnOwner = Convert.ToInt32(Session["sUserId"].ToString());
              



                strReturnData = objBll.saveRecords();
                
                

                if (strReturnData != "")
                {
                    this.lblMessage.Text = "Data have been saved successfully.";
                    this.lblMessage.ForeColor = System.Drawing.Color.Black;


                    PhotoBase64.Src = null;
                    PhotoBase64.DataBind();

                    this.tblMessage.Visible = true;

                   

                    clsSqlHelp.emptyControls(this);

                Response.Redirect("frmDiagnosisDetails.aspx?qsVisitorId="+ strReturnData);
                }
                else
                {
                    this.lblMessage.Text = "Error, Please contact system administrator!!!";
                    this.lblMessage.ForeColor = System.Drawing.Color.Red;

                    this.tblMessage.Visible = true;
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {

            }

    }
    private void readData()
    {
        try
        {
            string ef_idn_cn = Request.Params["ef_idn_cn"];
            if (ef_idn_cn == "")
            {
                Response.Redirect("frmInsertCardReader.aspx");
            }
            string ef_non_mod_data = Request.Params["ef_non_mod_data"];
            string ef_mod_data = Request.Params["ef_mod_data"];
            string ef_sign_image = Request.Params["ef_sign_image"];
            string ef_photo = Request.Params["ef_photo"];
            string ef_root_cert = Request.Params["ef_root_cert"];
            string ef_home_address = Request.Params["ef_home_address"];
            string ef_work_address = Request.Params["ef_work_address"];

            string certsPath = Request.MapPath("~/data_signing_certs");

            bool nonMod = true;
            bool mod = true;
            bool signImage = true;
            bool photo = true;
            bool homeAddress = true;
            bool workAddress = true;
            PublicDataParser parser = null;

            try
            {
                parser = new PublicDataParser(ef_idn_cn, certsPath);
                nonMod = parser.parseNonModifiableData(ef_non_mod_data);
                mod = parser.parseModifiableData(ef_mod_data);
                photo = parser.parsePhotography(ef_photo);
                signImage = parser.parseSignatureImage(ef_sign_image);
                homeAddress = parser.parseHomeAddressData(ef_home_address);
                workAddress = parser.parseWorkAddressData(ef_work_address);
                parser.parseRootCertificate(ef_root_cert);
            }
            catch (Exception ex)
            {

            }

            //NonMod.Text = nonMod.ToString();
            //Mod.Text = mod.ToString();
            //SignImage.Text = "".Equals(ef_sign_image) ? "N/A" : signImage.ToString();
            //Photo.Text = "".Equals(ef_photo) ? "N/A" : photo.ToString();
            //HomeAddress.Text = "".Equals(ef_home_address) ? "N/A" : homeAddress.ToString();
            //WorkAddress.Text = "".Equals(ef_work_address) ? "N/A" : workAddress.ToString();

            //NAME DETAILS :: START

            //FullName_ar.Text = PublicDataUtils.RemoveCommas(parser.getArabicFullName());
            this.cardHolderNameAr.Text = PublicDataUtils.RemoveCommas(parser.getArabicFullName());

            //lblWelcome.Text = "أهلا بك " + @"""" + this.txtFullNameAr.Text + @"""";

            //FullName.Text = PublicDataUtils.RemoveCommas(parser.getFullName());
            cardHolderNameEn.Text = PublicDataUtils.RemoveCommas(parser.getFullName());
            //MaritalStatus.Text = parser.getMaritalStatus();
            string varMaritalStatus = parser.getMaritalStatus();
            ViewState["vsMaritalStatus"] = varMaritalStatus;
            //MaritalStatusDesc.Text = PublicDataUtils.GetMaritalStatus(varMaritalStatus);
            //txtMaritalStatus.Text = PublicDataUtils.GetMaritalStatus(varMaritalStatus);

            string varPobEn = parser.getPlaceOfBirth();
            string varPobAr = parser.getPlaceOfBirth_ar();
            //this.lblPlaceOfBirth.Text = varPobAr;
            //this.txtPlaceofBith.Text = varPobAr;
            //DoB.Text = parser.getDateOfBirth() == null ? "" : parser.getDateOfBirth().Value.ToString("dd/MM/yyyy");
            txtDob.Text = parser.getDateOfBirth() == null ? "" : parser.getDateOfBirth().Value.ToString("dd/MM/yyyy");
            //Sex.Text = parser.getSex();
            Session["varSex"] = parser.getSex();
            string varSexDescn = PublicDataUtils.GetSex(parser.getSex());
            //SexDescn.Text = varSexDescn;
            //txtSex.Text = varSexDescn;


            //DAUBTS :: START
            string varTitleEn = parser.getTitle();//NULL
            string varTitleAr = parser.getArabicTitle();//NULL
            //drpTitle.SelectedIndex = drpTitle.Items.IndexOf(drpTitle.Items.FindByText(varTitleAr));
            //if (drpTitle.SelectedIndex == 0)
            //{
            //    drpTitle.SelectedIndex = drpTitle.Items.IndexOf(drpTitle.Items.FindByValue(varTitleAr));
            //}
            //DAUBTS :: END

            //NAME DETAILS :: END

            //SPONSOR DETAILS :: START
            //SponsorType.Text = parser.getSponsorType();
            ViewState["vsSponsorType"] = parser.getSponsorType();
            try
            {
                //SponsorType.Text = PublicDataUtils.GetSponsorType(parser.getSponsorType());
                ViewState["vsSponsorType"] = PublicDataUtils.GetSponsorType(parser.getSponsorType());
            }
            catch (Exception ex)
            {

            }

            //SponsorName.Text = parser.getSponsorName();
            ViewState["vsSponsorName"] = parser.getSponsorName();

            //SponsorUnifiedNumber.Text = parser.getSponsorUnifiedNumber();
            ViewState["vsSponUnifiedNumber"] = parser.getSponsorUnifiedNumber();
            //SPONSOR DETAILS :: END

            //WORK ADDRESS DETAILS :: START
            string varWorkAddressAreaCode = parser.getWorkAddressAreaCode();
            string varWorkAddressAreaDescEn = parser.getWorkAddressAreaDesc();
            string varWorkAddressAreaDescAr = parser.getWorkAddressAreaDesc_ar();
            string varWorkAddressBuildingNameEn = parser.getWorkAddressBuildingName();
            string varWorkAddressBuildingNameAr = parser.getWorkAddressBuildingName_ar();
            string varWorkAddressCityCode = parser.getWorkAddressCityCode();
            string varWorkAddressCityDescEn = parser.getWorkAddressCityDesc();
            string varWorkAddressCityDescAr = parser.getWorkAddressCityDesc_ar();

            string varWorkAddressCompanyNameAr = parser.getWorkAddressCompanyName_ar();

            //this.txtCompanyName.Text = varWorkAddressCompanyNameAr;

            string varWorkAddressCompanyNameEn = parser.getWorkAddressCompanyName();

            //if (txtCompanyName.Text.Trim() == "")
            //{
            //    this.txtCompanyName.Text = varWorkAddressCompanyNameEn;
            //}

            string varWorkAddressEmail = parser.getWorkAddressEmail();
            string varWorkAddressEmiratesCode = parser.getWorkAddressEmirateCode();
            string varWorkAddressEmiratesDescEn = parser.getWorkAddressEmirateDesc();
            string varWorkAddressEmiratesDescAr = parser.getWorkAddressEmirateDesc_ar();

            string varWorkAddressLandPhoneNo = parser.getWorkAddressLandPhoneNo();
            string varWorkAddressLocationCode = parser.getWorkAddressLocationCode();
            string varWorkAddressMobilePhoneNo = parser.getWorkAddressMobilePhoneNo()
                ;
            string varWorkAddressPoBox = parser.getWorkAddressPOBox();
            string varWorkAddressStreetEn = parser.getWorkAddressStreet();
            string varWorkAddressStreetAr = parser.getWorkAddressStreet_ar();
            string varWorkAddressType = parser.getWorkAddressTypeCode();
            //WORK ADDRESS DETAILS :: END

            //NATIONALITY DETAILS :: START
            //Nationality_ar.Text = parser.getArabicNationality();
            //this.txtNationality.Text = parser.getArabicNationality();
            //Nationality.Text = parser.getNationality();
            ViewState["vsNationality"] = parser.getNationality();

            //NATIONALITY DETAILS :: END

            //OCCUPATION DETAILS :: START
            //Occupation.Text = parser.getOccupation() == null ? "" : parser.getOccupation();
            ViewState["vsOccupation"] = parser.getOccupation() == null ? "" : parser.getOccupation();//Job
            try
            {
                //Occupation.Text = PublicDataUtils.GetOccupation(Occupation.Text);
                ViewState["vsOccupation"] = PublicDataUtils.GetOccupation(ViewState["vsOccupation"].ToString());
            }
            catch (Exception ex)
            {

            }
            //this.txtOccupation.Text = ViewState["vsOccupation"].ToString();

            string varOccupationDescn = PublicDataUtils.GetOccupation(ViewState["vsOccupation"].ToString());


            //OccupationField.Text = parser.getOccupationField() == null ? "" : parser.getOccupationField();
            ViewState["vsOccupationField"] = parser.getOccupationField() == null ? "" : parser.getOccupationField();
            string varOccupationTypeEn = parser.getOccupationType();

            //drpOccupationField.SelectedIndex = drpOccupationField.Items.IndexOf(drpOccupationField.Items.FindByValue(ViewState["vsOccupationField"].ToString()));

            string varOccupationTypeAr = parser.getOccupationType_ar();

            //OCCUPATION DETAILS :: END

            //PASSPORT DETAILS :: START
            //lblPassportNumberValue.Text = parser.getPassportNumber();
            //txtPassportNumber.Text = parser.getPassportNumber();
            //lblPassportIssueDate.Text = parser.getPassportIssueDate() == null ? "" : parser.getPassportIssueDate().Value.ToString("dd/MM/yyyy");
            //txtPassportIssueDate.Text = parser.getPassportIssueDate() == null ? "" : parser.getPassportIssueDate().Value.ToString("dd/MM/yyyy");
            //lblPassportExpiryDate.Text = parser.getPassportExpiryDate() == null ? "" : parser.getPassportExpiryDate().Value.ToString("dd/MM/yyyy");
            //txtPassportExpiryDate.Text = parser.getPassportExpiryDate() == null ? "" : parser.getPassportExpiryDate().Value.ToString("dd/MM/yyyy");
            //lblPassportType.Text = parser.getPassportType(); ;
            ViewState["vsPassportType"] = parser.getPassportType();
            //lblPassportCountryCode.Text = parser.getPassportCountry();
            ViewState["vsPassportCntryCode"] = parser.getPassportCountry();
            string varPassportCountryDescEn = parser.getPassportCountryDesc();
            //lblPassportCountry.Text = parser.getPassportCountryDesc_ar();
            //txtPassportCountry.Text = parser.getPassportCountryDesc_ar();
            //PASSPORT DETAILS :: END



            //MOTHER DETAILS :: START
            string varArabicMontherFirstName = parser.getArabicMotherFirstName();
            string varMotherName = parser.getMotherFullName() == null ? "" : parser.getMotherFullName();
            //lblMotherName.Text = parser.getMotherFullName_ar() == null ? "" : parser.getMotherFullName_ar();

            //txtMotherName.Text = parser.getMotherFullName_ar() == null ? "" : parser.getMotherFullName_ar();
            string varMotherFirstName = parser.getMotherFirstName();
            //MOTHER DETAILS :: END

            //CARD DETAILS :: START
            //IDN.Text = parser.getIdNumber();
            idn.Text = parser.getIdNumber();
            //CardNumber.Text = parser.getCardNumber();
            //txtCardNumber.Text = parser.getCardNumber();
            //ExpiryDate.Text = parser.getExpiryDate().Value.ToString("dd/MM/yyyy");

            //IdType.Text = parser.getIdType();
            ViewState["vsIdType"] = parser.getIdType();
            //IssueDate.Text = parser.getIssueDate().Value.ToString("dd/MM/yyyy");
            //txtIdIssueDate.Text = parser.getIssueDate().Value.ToString("dd/MM/yyyy");
            //txtIdExpiryDate.Text = parser.getExpiryDate().Value.ToString("dd/MM/yyyy");
            //CARD DETAILS :: END

            //FAMILY DETAIL :: START
            //string varFamilyId = parser.getFamilyID();
            //lblFamilyId.Text = parser.getFamilyID();
            //txtFamilyId.Text = parser.getFamilyID();
            //FAMILY DETAIL :: END

            //STUDY DETAILS :: START
            string varFieldStudyEn = parser.getFieldOfStudy();
            string varFieldStudyAr = parser.getFieldOfStudy_ar();
            //lblFieldStudyAr.Text = varFieldStudyAr;
            //drpFieldOfStudy.SelectedIndex = drpFieldOfStudy.Items.IndexOf(drpFieldOfStudy.Items.FindByValue(parser.getFieldOfStudyCode()));
            //if (drpFieldOfStudy.SelectedIndex == 0)
            //{
            //    drpFieldOfStudy.SelectedIndex = drpFieldOfStudy.Items.IndexOf(drpFieldOfStudy.Items.FindByText(varFieldStudyAr));
            //}
            //lblFieldStudyCodeValue.Text = 
            //STUDY DETAILS :: START

            //COMPANY DETAILS :: START
            string varCompanynameAr = parser.getCompanyName_ar();
            //if (varCompanynameAr != "")
            //{
            //    this.txtCompanyName.Text = varCompanynameAr;
            //}

            string varCompanyName = parser.getCompanyName();
            //if (txtCompanyName.Text.Trim() == "")
            //{
            //    txtCompanyName.Text = varCompanyName;
            //}

            //COMPANY DETAILS :: END

            //DEGREE DETAILS :: START
            string varDegreeEn = parser.getDegreeDesc();
            string varDegreeAr = parser.getDegreeDesc_ar();
            //this.txtDegreeDescnValue.Text = parser.getDegreeDesc_ar();

            string varGraduationDate = parser.getGraduationDate() == null ? "" : parser.getGraduationDate().Value.ToString("dd/MM/yyyy");
            string varPlaceOfStudy = parser.getPlaceOfStudy();
            string varPlaceOfStudyEn = parser.getPlaceOfStudy_ar();

            string varQualificationLevel = parser.getQualificationLevel();
            //lblQualificationLevelCode.Text = varQualificationLevel;
            //this.drpQualification.SelectedIndex = this.drpQualification.Items.IndexOf(this.drpQualification.Items.FindByValue(varQualificationLevel));
            string varQualificationDescEn = parser.getQualificationLevelDesc();
            string varQualificationDescAr = parser.getQualificationLevelDesc_ar();
            //lblQualificationLevelCodeDetails.Text = parser.getQualificationLevelDesc_ar();
            //DEGREE DETAILS :: END

            //RESIDENCY DETAILS :: START
            //ResidencyType.Text = parser.getResidencyType();
            ViewState["vsResType"] = parser.getResidencyType();

            try
            {
                string varResTypeEn = PublicDataUtils.GetResidencyType(ViewState["vsResType"].ToString());
            }
            catch (Exception ex)
            {
            }

            //ResidencyNumber.Text = parser.getResidencyNumber();
            //ResidencyExpiryDate.Text = parser.getResidencyExpiryDate() == null ? "" : parser.getResidencyExpiryDate().Value.ToString("dd/MM/yyyy");

            //RESIDENCY DETAILS :: END


            //ADDRESS DETAILS :: START

            string varHomeAddressAreaCode = parser.getHomeAddressAreaCode();
            //this.txtAreaCode.Text = varHomeAddressAreaCode;
            //if (this.txtAreaCode.Text.Trim() == "")
            //{
            //    this.txtAreaCode.Text = varWorkAddressAreaCode;
            //}
            string VvarHomeAddressAreaEn = parser.getHomeAddressAreaDesc();
            string VvarHomeAddressAreaAr = parser.getHomeAddressAreaDesc_ar();
            //this.txtAreaDescnAr.Text = VvarHomeAddressAreaAr;
            //if (this.txtAreaDescnAr.Text.Trim() == "")
            //{
            //    this.txtAreaDescnAr.Text = varWorkAddressAreaDescAr;
            //}
            string varHomeAddressBuildingNameEn = parser.getHomeAddressBuildingName();
            string varHomeAddressBuildingNameAr = parser.getHomeAddressBuildingName_ar();
            //this.txtBuilding.Text = varHomeAddressBuildingNameAr;
            //if (this.txtBuilding.Text.Trim() == "")
            //{
            //    this.txtBuilding.Text = varWorkAddressBuildingNameAr;
            //}
            string varHomeAddressCityCode = parser.getHomeAddressCityCode();
            //this.txtCityCode.Text = varHomeAddressCityCode;
            //if (this.txtCityCode.Text.Trim() == "")
            //{
            //    this.txtCityCode.Text = varWorkAddressCityCode;
            //}
            string varHomeAddressCityDescriptionEn = parser.getHomeAddressCityDesc();
            string varHomeAddressCityDescriptionAr = parser.getHomeAddressCityDesc_ar();

            //this.txtCityDescn.Text = varHomeAddressCityDescriptionAr;
            //if (this.txtCityDescn.Text.Trim() == "")
            //{
            //    this.txtCityDescn.Text = varWorkAddressCityDescAr;
            //}

            string varHomeAddressEmail = parser.getHomeAddressEmail();

            //EMIRATES :: START
            string varHomeAddressEmirateCode = parser.getHomeAddressEmirateCode();
            string varHomeAddressEmirateEn = parser.getHomeAddressEmirateDesc();
            string varHomeAddressEmirateAr = parser.getHomeAddressEmirateDesc_ar();

            //this.drpEmirates.SelectedIndex = this.drpEmirates.Items.IndexOf(this.drpEmirates.Items.FindByValue(varHomeAddressEmirateCode));
            //if (this.drpEmirates.SelectedIndex == 0)
            //{
            //    this.drpEmirates.SelectedIndex = this.drpEmirates.Items.IndexOf(this.drpEmirates.Items.FindByValue(varWorkAddressEmiratesCode));
            //}
            //EMIRATES :: END

            string varHomeAddressFlatNo = parser.getHomeAddressFlatNo();
            //this.txtFlatNo.Text = varHomeAddressFlatNo;
            string varHomeAddressLocationCode = parser.getHomeAddressLocationCode();
            //this.txtLocationCode.Text = varHomeAddressLocationCode;
            //if (this.txtLocationCode.Text.Trim() == "")
            //{
            //    this.txtLocationCode.Text = varWorkAddressLocationCode;
            //}
            //string varHomeAddressMobilePhone = parser.getHomeAddressMobilePhoneNo();
            //visitor_mobile.Text = parser.getHomeAddressMobilePhoneNo();
            //this.txtMobilePhoneNo.Text = varHomeAddressMobilePhone;
            //if (this.txtMobilePhoneNo.Text.Trim() == "")
            //{
            //    this.txtMobilePhoneNo.Text = varWorkAddressMobilePhoneNo;
            //}

            string varHomeAddressPoBox = parser.getHomeAddressPOBox();
            //this.txtPoBox.Text = varHomeAddressPoBox;
            //if (this.txtPoBox.Text.Trim() == "")
            //{
            //    this.txtPoBox.Text = varWorkAddressPoBox;
            //}
            string varHomeAddressResPhoneNumber = parser.getHomeAddressResidentPhoneNo();

            this.visitor_mobile.Text = varHomeAddressResPhoneNumber;

            if (this.visitor_mobile.Text.Trim() == "")
            {
                this.visitor_mobile.Text = varWorkAddressLandPhoneNo;
            }
            if (this.visitor_mobile.Text.Trim() == "")
            {
                visitor_mobile.Text = parser.getHomeAddressMobilePhoneNo();
            }

                string varHomeAddressStreetEn = parser.getHomeAddressStreet();

            string varHomeAddressStreetAr = parser.getHomeAddressStreet_ar();
            //this.txtStreetAr.Text = varHomeAddressStreetAr;
            //if (this.txtStreetAr.Text.Trim() == "")
            //{
            //    this.txtStreetAr.Text = varWorkAddressStreetAr;
            //}

            string varHomeAddressTypeCode = parser.getHomeAddressTypeCode();
            //this.txtAddressTypeCode.Text = varHomeAddressTypeCode;
            //if (this.txtAddressTypeCode.Text.Trim() == "")
            //{
            //    this.txtAddressTypeCode.Text = varWorkAddressType;
            //}
            //ADDRESS DETAILS :: END

            //HUSBAND DETAILS :: START
            string varHusbandId = parser.getHusbandIDN();
            //lblHusbandIdn.Text = varHusbandId;
            //txtHusbandIdn.Text = varHusbandId;
            //HUNBAND DETAILS :: END

            //EMAIL ADDRESS :: START
            try
            {
                //if (parser.getHomeAddressEmail().Trim() != "")
                //{
                //    this.txtEmail.Text = parser.getHomeAddressEmail();
                //    this.txtEmail.ReadOnly = true;
                //}
                //else if (parser.getWorkAddressEmail().Trim() != "")
                //{
                //    this.txtEmail.Text = parser.getWorkAddressEmail();
                //    this.txtEmail.ReadOnly = true;
                //}
            }
            catch (Exception ex)
            {

            }
            //EMAIL ADDRESS :: END

            //PHOTO GRAPHY :: START
            if (parser.getPhotography() != null)
            {
                ViewState["vsImageByte"] = Convert.ToBase64String(parser.getPhotography());
                PhotoBase64.Src = "data:image/jpeg;base64," + ViewState["vsImageByte"].ToString();
            }
            //PHOTOGRAPHY :: END

            //SIGNATURE :: START

            //if (parser.getHolderSignatureImage() != null)
            //{
                //ViewState["vsSignatureByte"] = Convert.ToBase64String(parser.getHolderSignatureImage());
                //imgSignature.Src = "data:image/tiff;base64," + Convert.ToBase64String(parser.getHolderSignatureImage());
            //}

            //SIGNATURE :: END
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["sUserId"] = "1";//DANGER
        if (Session["sUserId"] == null)
        {
            Response.Redirect("~/frmLogout.aspx");
        }

        //imgBtnAdd.Attributes.Add("onClick", "javascript:return validateControls();");
        this.cardHolderNameEn.Attributes.Add("onkeypress", "javascript:return hideMessage();");
        this.cardHolderNameAr.Attributes.Add("onkeypress", "javascript:return hideMessage();");
        if (!IsPostBack)
        {
            this.tblMessage.Visible = false;
            //objBll.fillCombo(drpCompanyName, 0,0);
            objBll.fillCombo(drpNationality, 0,0);
            objBll.fillCombo(drpCategory, 0, 0);
            //objBll.fillCombo(drpSector, 0,0);
            //objBll.fillCombo(drpVisitingPlace, 0,0);
            //objBll.fillCombo(drpUsers, 0,0);
            //ViewState["loaded"] = true;

            //this.txtCheckInTime.Text = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString() + " " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();


            if (Request.QueryString["qsVisitId"] != null)
            {
                ViewState["vsVisitId"] = Request.QueryString["qsVisitId"].ToString();
                loadTestCompleteDetails(Convert.ToInt32(ViewState["vsVisitId"].ToString()));
            }
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Write("<script>window.open('frmLogin.aspx?qsLogout=1','_parent');</script>");
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmDashboard.aspx");
    }
    protected void imgBtnReadPublicData_Click(object sender, ImageClickEventArgs e)
    {

        //    string varMsg = callAPI();

        //    this.idn.Text = JObject.Parse(varMsg)["idn"].ToString();
        //    this.cardHolderNameAr.Text = JObject.Parse(varMsg)["cardHolderNameAr"].ToString();
        //    this.cardHolderNameEn.Text = JObject.Parse(varMsg)["cardHolderNameEn"].ToString();

        //    if (JObject.Parse(varMsg)["gender"].ToString().ToUpper() == "M")
        //    {
        //        this.radGender.Items[0].Selected = true;
        //    }
        //    else
        //    {
        //        this.radGender.Items[1].Selected = true;
        //    }
        //    string[] sDobs = JObject.Parse(varMsg)["dob"].ToString().Split('-');
        //    this.txtDob.Text = sDobs[2] + "/" + sDobs[1] + "/" + sDobs[0];
        //    //this.txtCardNumber.Text = JObject.Parse(varMsg)["cardNumber"].ToString();
        //    //this.txtCardIssueDate.Text = JObject.Parse(varMsg)["cardIssueDate"].ToString();
        //    //this.txtCardExpiryDate.Text = JObject.Parse(varMsg)["cardExpiryDate"].ToString();
        //    this.txtPassportNumber.Text = JObject.Parse(varMsg)["passportNumber"].ToString();
        //    //this.txtPassportIssueDate.Text = JObject.Parse(varMsg)["passportIssueDate"].ToString();
        //    //this.txtPassportExpiryDate.Text = JObject.Parse(varMsg)["passportExpiryDate"].ToString();
        //    visitor_mobile.Text = JObject.Parse(varMsg)["mobileNumber"].ToString();
        //    this.txtEmail.Text = JObject.Parse(varMsg)["email"].ToString();
        //    //this.txtPoBox.Text = JObject.Parse(varMsg)["poBox"].ToString();
        //    this.txtAddress.Text = JObject.Parse(varMsg)["homeAddress"].ToString();
        //    this.txtJobInfo.Text = JObject.Parse(varMsg)["occupationFiled"].ToString();
        //    //PHOTO GRAPHY :: START
        //    PhotoBase64.Src = "data:image/jpeg;base64," + JObject.Parse(varMsg)["portraitBase64"].ToString();
        //    //PHOTOGRAPHY :: END

        //    ViewState["vsImageByte"] = JObject.Parse(varMsg)["portraitBase64"].ToString();

        //    //SIGNATURE :: START
        //    //imgSignature.Src = "data:image/jpeg;base64," + JObject.Parse(varMsg)["signatureBase64"].ToString();
        //    //"data:image/tiff;base64," + JObject.Parse(varMsg)["signatureBase64"].ToString();

        //    //SIGNATURE :: END

        //    //1982 - 04 - 03

        //    //this.txtDob.Text = JObject.Parse(varMsg)["dob"].ToString();
        //    //JObject.Parse(varMsg)["cardNumber"].ToString();
        //    //JObject.Parse(varMsg)["cardExpiryDate"].ToString();
        //    //JObject.Parse(varMsg)["cardIssueDate"].ToString();
        //    this.drpNationality.SelectedIndex = this.drpNationality.Items.IndexOf(this.drpNationality.Items.FindByValue(JObject.Parse(varMsg)["nationalityCode"].ToString()));
        //    //JObject.Parse(varMsg)["nationalityArabicDesc"].ToString();
        //    //JObject.Parse(varMsg)["passportExpiryDate"].ToString();
        //    //JObject.Parse(varMsg)["passportIssueDate"].ToString();
        //    //JObject.Parse(varMsg)["passportNumber"].ToString();
        //    //JObject.Parse(varMsg)["fingerPrintIndex1"].ToString();
        //    //JObject.Parse(varMsg)["fingerPrintIndex2"].ToString();


        //    JObject.Parse(varMsg)["qualification"].ToString();
        //    JObject.Parse(varMsg)["occupation"].ToString();
        //if (checkPendingTest() == "")//IF THERE IS NO PENDING TEXT
        //{
        //    //NO PENDING TEST
        //}
        //else
        //{
        //    Response.Redirect("frmViewVisitors.aspx");
        //}

    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("frmDashboard.aspx");
    }



    protected void btnSave_Click(object sender, EventArgs e)
    {
        //System.Threading.Thread.Sleep(750);
        saveVisitor(1);
    }

    protected void btnRegister_Click(object sender, EventArgs e)
    {
        clsSqlHelp.emptyControls(this);
        this.PhotoBase64.Src = null;
        this.PhotoBase64.DataBind();
    }

    protected void drpUnits_SelectedIndexChanged(object sender, EventArgs e)
    {
        //loadEmployee();

        //drpUnits.Focus();
    }


  


    protected void btnReadEmiratesIdCard_Click(object sender, EventArgs e)
    {
        imgBtnReadPublicData_Click(null, null);
    }

    protected void chkTestOneEye_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void chkTestOneHeight_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void chkTestOneWeight_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void chkTestTwoBlood_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void chkTestTwoUrine_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void chkTestThreeXray_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void chkTestFourEye_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void chkTestFourBone_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void chkTestFourSkin_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void chkTestFourPsych_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void chkTestFourSurgery_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void chkTestFourGp_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void radFitnessSelection_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}



