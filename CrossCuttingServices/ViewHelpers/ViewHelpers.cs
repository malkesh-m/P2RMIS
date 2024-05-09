using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using CsQuery.ExtensionMethods.Internal;
using Sra.P2rmis.CrossCuttingServices.ConfigurationServices;

namespace Sra.P2rmis.CrossCuttingServices
{
    /// <summary>
    /// Provides common functionality for views to centralize how things
    /// are done.
    /// </summary>
    public static class ViewHelpers
    {
        #region Constants
        /// <summary>
        /// ViewHelper constants.
        /// </summary>
        public class Constants
        {
            /// <summary>
            /// The round to one decimal places constant
            /// </summary>
            public static int RoundToOneDecimalPlace = 1;
            /// <summary>
            /// The round to two decimal places constant
            /// </summary>
            public static int RoundToTwoDecimalPlaces = 2;
            /// <summary>
            /// Key value for a success save of account information
            /// </summary>
            public const string AccountSuccessMessageKey = "AccountSuccessMessage";

            /// <summary>
            /// Separator between last and first name of principal investigator. 
            /// </summary>
            public const string NameSeparator = ", ";
            /// <summary>
            /// Score not applicable message. 
            /// </summary>
            public const string ScoreNotApplicableMessage = "N/A";
            /// <summary>
            /// The score not discussed
            /// </summary>
            public const string ScoreNotDiscussed = "ND";
            /// <summary>
            /// Score not submitted message. 
            /// </summary>
            public const string ScoreNotSubmittedMessage = "Not Submitted";
            ///
            /// Hard space
            ///
            public const string HardSpace = "&nbsp;";
            /// <summary>
            /// Key value for a successful save
            /// </summary>
            public const string SuccessMessageKey = "SuccessMessage";
            /// <summary>
            /// The verify password
            /// </summary>
            public const string VerifyPassword = "You must verify your account information before proceeding on the system.";
            /// <summary>
            /// Blinded
            /// </summary>
            public const string Blinded = "Blinded";
            /// <summary>
            /// Top number of partner PI records before cropping
            /// </summary>
            public const int TopNumberOfPartnerPis = 3;
            /// <summary>
            /// The abstained criterion label
            /// </summary>
            public const string Abstained = "Abstain";
        }
        #endregion
        #region Services Provided
        /// <summary>
        /// Provide common access to rounding.
        /// </summary>
        /// <param name="value">Value to round</param>
        /// <param name="decimals">Number of decimal places to round to</param>
        /// <param name="mode">Rounding mode</param>
        /// <returns>Value rounded to specified decimal places</returns>
        public static decimal Round(decimal value, int decimals, MidpointRounding mode)
        {
            return Math.Round(value, decimals, mode);
        }
        /// <summary>
        /// Provide rounding in a standardized manner.
        /// </summary>
        /// <param name="value">Value to round</param>
        /// <returns>Value rounded to specified decimal places</returns>
        public static decimal P2rmisRound(decimal value)
        {
            return ViewHelpers.Round(value, Constants.RoundToOneDecimalPlace, MidpointRounding.AwayFromZero);
        }
        /// <summary>
        /// Formats scores as a string for display
        /// </summary>
        /// <param name="value">Value to round</param>
        public static string ScoreFormatter(decimal? value)
        {
            return (!value.HasValue) ? Constants.ScoreNotDiscussed : P2rmisRound(value.Value).ToString();
        }
        /// <summary>
        /// Formats scores without rounding that have a known desired type as a string for display
        /// </summary>
        /// <param name="score">Numeric value of a score</param>
        /// <param name="scoreType">Type of the score</param>
        /// <param name="adjectival">Adjectival value of a score</param>
        public static string ScoreFormatterNotCalculated(decimal? score, string scoreType, string adjectival)
        {
            string formattedScore = String.Empty;
            if (score != null)
                formattedScore = ScoreFormatterNotCalculated((decimal)score, scoreType, adjectival, true);
            else
                formattedScore = Constants.ScoreNotApplicableMessage;
            return formattedScore;
        }
        /// <summary>
        /// Formats scores without rounding that have a known desired type as a string for display
        /// </summary>
        /// <param name="score">Numeric value of a score</param>
        /// <param name="scoreType">Type of the score</param>
        /// <param name="adjectival">Adjectival value of a score</param>
        /// <param name="isSubmitted">Adjectival value of a score</param>
        /// <remarks>
        /// There are a few scenarios we account for here
        /// 1) Score should be formatted appropriately according to whether it is Integer, Adjectival or Decimal
        /// 2) Score may not be submitted in which case not submitted message should display
        /// 3) Score may have been submitted but not exist from the reviewer (some reviewers are asked to submit overall) in which case not applicable message should display
        /// 4) Application may not be scored at all in which case not applicable message should display
        /// </remarks>
        public static string ScoreFormatterNotCalculated(decimal score, string scoreType, string adjectival, bool isSubmitted)
        {
            string formattedScore = String.Empty;
            if (!isSubmitted)
            {
                formattedScore = Constants.ScoreNotSubmittedMessage;
            }
            else if (score > 0)
            {
                if (string.Equals(scoreType, "Adjectival", StringComparison.OrdinalIgnoreCase))
                {
                    formattedScore = adjectival;
                }
                else if (string.Equals(scoreType, "Decimal", StringComparison.OrdinalIgnoreCase))
                {
                    formattedScore = FormatScoreDecimal(score);
                }
                else
                {
                    formattedScore = FormatScoreInteger(score);
                }
            }
            else
                formattedScore = Constants.ScoreNotApplicableMessage;
            return formattedScore;
        }
        /// <summary>
        /// Formats scores without rounding that have a known desired type as a string for display. Does not include status into logic (likely status is already shown elsewhere).
        /// </summary>
        /// <param name="score">Numeric value of a score</param>
        /// <param name="scoreType">Type of the score</param>
        /// <param name="adjectival">Adjectival value of a score</param>
        /// <param name="isSubmitted">Adjectival value of a score</param>
        /// <remarks>
        /// Close in format to ScoreFormatterNotCalculated, however String.empty is used as opposed to a message. Also is not overridden by whether critique is submitted.
        /// </remarks>
        public static string ScoreFormatterNotCalculatedNoStatus(decimal score, string scoreType, string adjectival, bool isSubmitted)
        {
            string formattedScore = String.Empty;
            if (score > 0)
            {
                if (string.Equals(scoreType, "Adjectival", StringComparison.OrdinalIgnoreCase))
                    formattedScore = adjectival;
                else if (string.Equals(scoreType, "Decimal", StringComparison.OrdinalIgnoreCase))
                    formattedScore = FormatScoreDecimal(score);
                else
                    formattedScore = FormatScoreInteger(score);
            }
            return formattedScore;
        }
        /// <summary>
        /// Provide rounding in a standardized manner.
        /// </summary>
        /// <param name="value">Value to round</param>
        /// <returns>Value rounded to specified decimal places</returns>
        public static decimal P2rmisRoundTwoDecimalPlaces(decimal value)
        {
            return ViewHelpers.Round(value, Constants.RoundToTwoDecimalPlaces, MidpointRounding.AwayFromZero);
        }
        /// <summary>
        /// Constructs the principal investigator name from the last name and a first name.
        /// </summary>
        /// <param name="lastName">Principal investigator's last name</param>
        /// <param name="firstName">Principal investigator's first name</param>
        /// <returns>Principal investigator name</returns>
        public static string ConstructName(string lastName, string firstName)
        {
            StringBuilder builder = new StringBuilder();
            return builder.Append(lastName).Append(Constants.NameSeparator).Append(firstName).ToString();
        }
        /// <summary>
        /// Constructs the principal investigator name from the last name and a first name.
        /// </summary>
        /// <param name="firstName">Principal investigator's first name</param>
        /// <param name="lastName">Principal investigator's last name</param>
        /// <param name="suffix">Suffix</param>
        /// <returns>Principal investigator name</returns>
        public static string ConstructNameWithSuffix(string firstName, string lastName, string suffix)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(lastName).Append(Constants.NameSeparator).Append(firstName);
            if (!string.IsNullOrWhiteSpace(suffix))
            {
                builder.Append(Constants.NameSeparator).Append(suffix);
            }
            return builder.ToString();
        }
        /// <summary>
        /// Constructs the reviewer name in the format Prefix. FirstName LastName.
        /// </summary>
        /// <param name="prefix">Reviewer's prefix</param>
        /// <param name="lastName">Reviewer's last name</param>
        /// <param name="firstName">Reviewer's first name</param>
        /// <returns>Principal investigator name</returns>
        public static string ConstructNameWithPrefix(string prefix, string firstName, string lastName)
        {
            return string.Format("{0} {1} {2}", prefix, firstName, lastName);
        }
        /// <summary>
        /// Constructs name in the format LastName, FirstName
        /// </summary>
        /// <param name="firstName">First Name</param>
        /// <param name="lastName">Last Name</param>
        /// <returns>LastName, FirstName</returns>
        public static string ConstructNameWithComma(string firstName, string lastName)
        {
            return (string.IsNullOrEmpty(firstName)) ? (lastName ?? string.Empty) : ((string.IsNullOrEmpty(lastName)) ? firstName :
                string.Format("{1}, {0}", firstName, lastName));
        }
        /// <summary>
        /// Constructs the name and role.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="role">The role.</param>
        /// <returns>FirstName, LastName (Role)</returns>
        public static string ConstructNameAndRole(string firstName, string lastName, string role)
        {
            return string.Format("{0} {1} ({2})", firstName, lastName, role);
        }
        /// <summary>
        /// Constructs name and organization in the format LastName, FirstName / Organization
        /// </summary>
        /// <param name="firstName">First name</param>
        /// <param name="lastName">Last Name</param>
        /// <param name="organization">LastName, FirstName / Organization</param>
        /// <returns></returns>
        public static string ConstructNameAndOrganization(string firstName, string lastName, string organization)
        {
            return ConstructNameWithComma(firstName, lastName) + (string.IsNullOrEmpty(organization) ? string.Empty : " / " + organization);
        }
        /// <summary>
        /// Constructs the reviewers name from the last name and a first name.
        /// </summary>
        /// <param name="lastName">Reviewers last name</param>
        /// <param name="firstName">Reviewers first name</param>
        /// <returns>Reviewers name</returns>
        public static string ConstructNameWithSpace(string firstName, string lastName)
        {
            return string.Format("{0} {1}", firstName, lastName);
        }
        /// <summary>
        /// Constructs the reviewers name from the last name and a first name.
        /// </summary>
        /// <param name="lastName">Reviewers last name</param>
        /// <param name="email"></param>
        /// <param name="firstName">Reviewers first name</param>
        /// <returns>Reviewers name</returns>
        public static string ConstructNameWithEmail(string firstName, string lastName, string email)
        {
            return string.Format("{0} {1} {2}", firstName, lastName, email);
        }
        /// <summary>
        /// Constructs the reviewers information.
        /// </summary>
        /// <param name="lastName">Reviewers last name</param>
        /// <param name="firstName">Reviewers first name</param>
        /// <param name="participantType">Reviewers participant type</param>
        /// <param name="slot">Reviewers slot</param>
        /// <param name="role">Reviewers role</param>
        /// <returns>Reviewers information</returns>
        public static string MakeReviewerInformation(string firstName, string lastName, string participantType, int? slot, string role)
        {
            return (slot == null) ? string.Format("{0} {1} ({2}) {3}", firstName, lastName, participantType, role).Trim() :
                string.Format("{0} {1} ({2} {3}) {4}", firstName, lastName, participantType, slot, role).Trim();
        }
        /// <summary>
        /// Places the value inside of parenthesis
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ValueInParenthesis(string value)
        {
            return string.Format("({0})", value);
        }        
        /// <summary>
        /// Constructs the scoreboard reviewers name from the last name and a first name.
        /// </summary>
        /// <param name="lastName">Reviewers last name</param>
        /// <param name="firstName">Reviewers first name</param>
        /// <returns>Reviewers name</returns>
        public static string MakeScoreboardReviewerName(string firstName, string lastName)
        {
            string firstCharacter = (firstName.Length >= 1) ? firstName.Substring(0, 1) : string.Empty;
            return string.Format("{0}. {1}", firstCharacter, lastName);
        }

        /// <summary>
        /// Ensures a string value is not null.
        /// </summary>
        /// <param name="value">String value</param>
        /// <returns>String if not null; otherwise empty string</returns>
        public static string SetNonNull(string value)
        {
            return (string.IsNullOrEmpty(value))? string.Empty: value;
        }
        /// <summary>
        /// Calculate standard deviation
        /// </summary>
        /// <param name="list">-----</param>
        /// <param name="meanValue">-----</param>
        /// <returns>-----</returns>
        public static decimal StandardDeviation(List<decimal> list, decimal meanValue)
        {
            decimal result = 0;
            //
            // For each entry in the list subtract the mean value & square it.
            // 
            for (int i = 0; i < list.Count; i++)
            {
                list[i] = list[i] - meanValue;
                list[i] = list[i] * list[i];
            }
            //
            // Sum up the list values & divide by the sample size (-1)
            // & take the square root.
            // 
            if (list.Count > 1)
            {
                decimal x = list.Sum() / (list.Count - 1);
                result = Convert.ToDecimal(Math.Sqrt(Convert.ToDouble(x)));
            }

            return result;
        }
        /// <summary>
        /// Determines the average of a list of numbers.
        /// </summary>
        /// <param name="list">Number list to average</param>
        /// <returns>Average of numbers; </returns>
        public static decimal Average(List<decimal> list)
        {
            decimal result = 0;

            if (list.Count > 0)
            {
                result = list.Sum()/list.Count;
            }
            return result;
        }
        /// <summary>
        /// Determines if the Session is open.  By definition a Session is 
        /// open if the Session OpenDate is greater than or equal to Today
        /// and the Session CloseDate is less than or equal to Today.
        /// </summary>
        /// <param name="openDate">Session open date</param>
        /// <param name="closeDate">Session close date</param>
        /// <returns>True if the Session is open; false otherwise</returns>
        public static bool IsOpen(DateTime openDate, DateTime closeDate)
        {
            return ((GlobalProperties.P2rmisDateToday >= openDate) && (GlobalProperties.P2rmisDateToday <= closeDate));
        }
        /// <summary>
        /// Split a semi colon delimited string into an array and return a string array
        /// </summary>
        /// <param name="StringToSplit">The semi colon delimited string to split into an array</param>
        /// <returns>A string array from the items of the semi colon delimited list</returns>
        public static String[] SplitDelimitedString(String StringToSplit)
        {
            if (!string.IsNullOrWhiteSpace(StringToSplit))
            {
                string[] result = StringToSplit.Split(';');
                return result;
            }
            else
            {
                String[] result = new string[0];
                return result;
            }
        }
        /// <summary>
        /// Determines if a critique has been submitted based on the submitted date
        /// </summary>
        /// <param name="DateSubmitted">Date submitted</param>
        /// <returns>True if critique has been submitted, otherwise false</returns>
        public static bool IsCritiqueSubmitted(DateTime? DateSubmitted)
        {
            if (DateSubmitted != null)
                return true;
            else
                return false;
        }
        /// <summary>
        /// Standardize date format
        /// </summary>
        /// <param name="date">Date to formate</param>
        /// <returns>If date has a value returns it in MM/dd/YYYY format; empty sting otherwise</returns>
        public static String FormatDate(Nullable<System.DateTime> date)
        {
            return ((date != null) && date.HasValue)? date.Value.ToShortDateString(): string.Empty;
        }
        /// <summary>
        /// Standardize date format for contracts
        /// </summary>
        /// <param name="date">Date to format</param>
        /// <returns>If date has a value returns it in dddd, MMMM dd, yyyy format; empty sting otherwise</returns>
        public static String FormatContractDate(Nullable<System.DateTime> date)
        {
            return ((date != null) && date.HasValue)? date.Value.ToString("dddd, MMMM dd, yyyy"): string.Empty;
        }
        /// <summary>
        /// Standardize date/time format
        /// </summary>
        /// <param name="dateTime">Date/time to format</param>
        /// <returns>If dateTime has value returns it in h:mm tt MM/dd/yyyy; empty string otherwise</returns>
        public static String FormatDateTime(Nullable<System.DateTime> dateTime)
        {
            return ((dateTime != null) && dateTime.HasValue) ? dateTime.Value.ToString("h:mm tt MM/dd/yyyy") : string.Empty;
        }
        /// <summary>
        /// Formats the date time2.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        /// <remarks>TODO: standardize datetime format from the requirement team</remarks>
        public static String FormatDateTime2(Nullable<System.DateTime> dateTime)
        {
            return ((dateTime != null) && dateTime.HasValue) ? dateTime.Value.ToString("MM/dd/yyyy hh:mm:ss tt") : string.Empty;
        }
        /// <summary>
        /// Formats the date time in the format of "M/d/yyyy hh:mm tt"
        /// </summary>
        /// <param name="dateTime">The date/time in DateTime format.</param>
        /// <returns></returns>
        public static String FormatDateTime3(Nullable<System.DateTime> dateTime)
        {
            return ((dateTime != null) && dateTime.HasValue) ? dateTime.Value.ToString("M/d/yyyy hh:mm tt") : string.Empty;
        }
        /// <summary>
        /// Standardize date/time format
        /// </summary>
        /// <param name="dateTime">Date/time to format</param>
        /// <returns>If dateTime has value returns it in h:mm tt MM/dd/yyyy; empty string otherwise</returns>
        public static String FormatEstDateTime(Nullable<System.DateTime> dateTime)
        {
            return ((dateTime != null) && dateTime.HasValue) ? dateTime.Value.ToString("M/d/yyyy h:mm:ss tt EST") : string.Empty;
        }
        /// <summary>
        /// Standardize date/time format
        /// </summary>
        /// <param name="dateTime">Date/time to format</param>
        /// <returns>If dateTime has value returns it in h:mm tt MM/dd/yyyy; empty string otherwise</returns>
        public static String FormatEtDateTime(Nullable<System.DateTime> dateTime)
        {
            return ((dateTime != null) && dateTime.HasValue) ? dateTime.Value.ToString("MM/dd/yyyy hh:mm tt ET") : string.Empty;
        }
        /// <summary>
        /// Standard panel management critique date/time format
        /// </summary>
        /// <param name="dateTime">Date/time to format</param>
        /// <returns>If dateTime has value returns it in h:mm tt MM/dd/yyyy; empty string otherwise</returns>
        public static String FormatCritiqueDateTime(Nullable<System.DateTime> dateTime)
        {
            //return ((dateTime != null) && dateTime.HasValue) ? dateTime.Value.ToString("MM/dd/yyyy h:mm tt") : string.Empty;

            return ((dateTime != null) && dateTime.HasValue) ? dateTime.Value.ToString("MM/dd/yyyy") + Constants.HardSpace + Constants.HardSpace + dateTime.Value.ToString("h:mm tt") : string.Empty;
        }
        /// <summary>
        /// Standard date/time format for last update date
        /// </summary>
        /// <param name="dateTime">Date/time to format</param>
        /// <returns>If dateTime has value returns it in MM/dd/yyyy hh:mm tt; empty string otherwise</returns>
        public static String FormatLastUpdateDateTime(Nullable<DateTime> dateTime)
        {
            return (dateTime != null) ? dateTime.Value.ToString("MM/dd/yyyy hh:mm:ss tt") : string.Empty;
        }
        /// <summary>
        /// Standardize session date format
        /// </summary>
        /// <param name="openDate">Session's open date</param>
        /// <param name="closeDate">Session's close date</param>
        /// <returns></returns>
        public static String FormatSessionDates(System.DateTime openDate, System.DateTime closeDate) 
        {
            return FormatDate(openDate) + " - " + FormatDate(closeDate);
        }
        /// <summary>
        /// Standard P2RMIS currency formatting
        /// </summary>
        /// <param name="currencyValue">Value to format</param>
        /// <returns>Value formatted as currency, empty string otherwise</returns>
        public static String FormatCurrency(Nullable<int> currencyValue)
        {
            return (currencyValue.HasValue) ? currencyValue.Value.ToString("C2", CultureInfo.CurrentCulture) : string.Empty;
        }
        /// <summary>
        /// Formats the currency.
        /// </summary>
        /// <param name="currencyValue">The currency value.</param>
        /// <returns></returns>
        public static String FormatCurrency(decimal? currencyValue) {
            return (currencyValue.HasValue) ? String.Format("{0:C}", currencyValue) : string.Empty;
        }
        /// <summary>
        /// Formats the application duration into months.  Duration is rounded up (ceiling) to the nearest month.
        /// </summary>
        /// <param name="duration">Application duration in years</param>
        /// <returns>Duration value in months</returns>
        public static String FormatDurationIntoMonths(Nullable<decimal> duration)
        {
            return string.Format("{0} Months", (duration.HasValue) ? Math.Round(duration.Value * 12, MidpointRounding.AwayFromZero) : 0);
        }
        /// <summary>
        /// Construct the displayed group title for summary statement grids
        /// </summary>
        /// <param name="program">Program abbreviation of summary group</param>
        /// <param name="fiscalYear">Fiscal year of summary group</param>
        /// <param name="panel">Panel of summary group</param>
        /// <param name="award">Award of summary group (if used as a search filter</param>
        /// <returns>Summary group title line</returns>
        public static String FormatSummaryGroupName(string program, string fiscalYear, string panel, string award)
        {
            string displayAward = !string.IsNullOrWhiteSpace(award) ? " - " + award : string.Empty;

            string result = string.Format("{0} - {1} - {2}{3}", program, fiscalYear, panel, displayAward);
            return result;
        }
        /// <summary>
        /// Construct the displayed group title for summary statement grids
        /// </summary>
        /// <param name="program">Program abbreviation of summary group</param>
        /// <param name="fiscalYear">Fiscal year of summary group</param>
        /// <param name="cycle">Cycle of summary group</param>
        /// <param name="panel">Panel of summary group</param>
        /// <param name="award">Award of summary group (if used as a search filter</param>
        /// <returns>Summary group title line</returns>
        public static String FormatSummaryGroupName(string program, string fiscalYear, int cycle, string panel, string award)
        {
            string displayAward = !string.IsNullOrWhiteSpace(award) ? " - " + award : string.Empty;

            string result = string.Format("{0} - {1} - Cycle - {2} - {3}{4}", program, fiscalYear, cycle, panel, displayAward);
            return result;
        }
        /// <summary>
        /// Construct the displayed group title for summary statement grid (progress)
        /// </summary>
        /// <param name="program">Program abbreviation of summary group</param>
        /// <param name="fiscalYear">Fiscal year of summary group</param>
        /// <param name="cycle">Cycle of summary group</param>
        /// <param name="panel">Panel of summary group</param>
        /// <param name="award">Award of summary group (if used as a search filter</param>
        /// <param name="firstName">User's first name (if used as a search filter)</param>
        /// <param name="lastName">User's last name (if used as a search filter)</param>
        /// <returns>Summary group title line</returns>
        public static String FormatSummaryGroupName(string program, string fiscalYear, int cycle, string panel, string award, string firstName, string lastName)
        {
            string partialGroupTitle = FormatSummaryGroupName(program, fiscalYear, cycle, panel, award);

            string tempName = ConstructName(lastName, firstName).Trim();
            string displayName = (tempName.Length != Constants.NameSeparator.Trim().Length) ? " - " + tempName : string.Empty;

            string result = string.Format("{0}{1}", partialGroupTitle, displayName);
            return result;
        }

        /// <summary>
        /// Construct the display string value for boolean values
        /// </summary>
        /// <param name="booleanValue">The boolean value</param>
        /// <returns>The display string value</returns>
        public static String FormatBoolean(bool booleanValue)
        {
            return (booleanValue) ? "Yes" : "No";
        }
        /// <summary>
        /// Construct the display string value for file size
        /// </summary>
        /// <param name="docSize">Size of the document.</param>
        /// <returns>
        /// The display string value
        /// </returns>
        public static string FormatFileSize(long docSize)
        {
            const double oneKB = 1024;
            const double oneMB = 1048576;

            string Size;
            double numSize;
            numSize = (double)docSize;

            if (numSize < oneKB)
            {
                Size = string.Format("({0:0.#} B)", numSize);
            }
            else if ( numSize < oneMB )
            {
                numSize /= oneKB;
                Size = string.Format("({0:0.#} KB)", numSize);                
            }
            else
            {
                numSize /= oneMB;
                Size = string.Format("({0:0.#} MB)", numSize);                
            }

            return Size;
        }
        /// <summary>
        /// Formats a decimal score to the appropriate value
        /// </summary>
        /// <param name="score">The score to be formatted</param>
        /// <returns>Formatted score</returns>
        public static string FormatScoreDecimal(decimal score)
        {
            return score.ToString("F1", CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Formats an integer score to the appropriate value dropping any trailing 0s
        /// </summary>
        /// <param name="score">The score to be formatted</param>
        /// <returns>Formatted score</returns>
        public static string FormatScoreInteger(decimal score)
        {
            return score.ToString("F0", CultureInfo.InvariantCulture);
        }
        #endregion
        /// <summary>
        /// Determines if the user is able to view a critique.
        /// </summary>
        /// <param name="contentExists">Boolean value indicating if the context exists</param>
        /// <param name="phaseStartDate">Phase's start date</param>
        /// <returns>True if the critique can be viewed; false otherwise</returns>
        public static bool IsAbleToViewCritique(bool contentExists, DateTime phaseStartDate)
        {
            return contentExists && phaseStartDate <= GlobalProperties.P2rmisDateTimeNow;
        }
        /// <summary>
        /// Determines if the user is able to reset a critique.
        /// </summary>
        /// <param name="contentExists">Boolean value indicating if the context exists</param>
        /// <param name="dateSubmitted">Date the content was submitted</param>
        /// <param name="stepOrder">Current step order</param>
        /// <param name="maxStepOrder">Largest step order in current workflow</param>
        /// <param name="startDate">Phase start date</param>
        /// <param name="endDate">Phase end date</param>
        /// <param name="reOpenStartDate">Re-open start date</param>
        /// <param name="reOpenEndDate">Re-open end date</param>
        /// <param name="hasManageCritiquesPermission">Whether the user has permission to modify critiques on behalf of another user</param>
        /// <returns>True if the critique can be reset; false otherwise</returns>
        public static bool IsAbleToResetCritique(bool contentExists, Nullable<DateTime> dateSubmitted, int stepOrder, int maxStepOrder, 
            DateTime startDate, DateTime endDate, DateTime? reOpenStartDate, DateTime? reOpenEndDate, bool hasManageCritiquesPermission)
        {
            //
            // TODO: refactor the time tests out & use WithInDates
            //
            var currentTime = GlobalProperties.P2rmisDateTimeNow;
            return ((hasManageCritiquesPermission) && (contentExists) && (dateSubmitted.HasValue) && (stepOrder == maxStepOrder) && 
                ((currentTime >= startDate && currentTime < endDate) || 
                (reOpenStartDate != null && currentTime >= reOpenStartDate && reOpenEndDate != null && currentTime < reOpenEndDate)));
        }
        /// <summary>
        /// Determines if the user is able to submit a critique.
        /// </summary>
        /// <param name="contentExists">Boolean value indicating if the context exists</param>
        /// <param name="dateSubmitted">Date the content was submitted</param>
        /// <param name="stepOrder">Current step order</param>
        /// <param name="maxStepOrder">Largest step order in current workflow</param>
        /// <param name="startDate">Phase start date</param>
        /// <param name="endDate">Phase end date</param>
        /// <param name="reOpenStartDate">Re-open start date</param>
        /// <param name="reOpenEndDate">Re-open end date</param>
        /// <param name="hasManageCritiquesPermission">Whether the user has permission to modify critiques on behalf of another user</param>
        /// <returns>True if the critique can be Submitted; false otherwise</returns>
        public static bool IsAbleToSubmitCritique(bool contentExists, Nullable<DateTime> dateSubmitted, int stepOrder, int maxStepOrder, DateTime startDate, DateTime endDate, DateTime? reOpenStartDate, DateTime? reOpenEndDate, bool hasManageCritiquesPermission)
        {
            return ((hasManageCritiquesPermission) && (contentExists) && (!dateSubmitted.HasValue) && (stepOrder == maxStepOrder) && WithInDates(dateSubmitted, startDate, endDate, reOpenStartDate, reOpenEndDate));
        }
        /// <summary>
        /// Helper checking if the critique is able to be edited.
        /// </summary>
        /// <param name="dateSubmitted">Date/time critique was edited</param>
        /// <param name="startDate">Date/time phase was started</param>
        /// <param name="endDate">Date/time phase was ended</param>
        /// <param name="reOpenStartDate">Date/time phase was reopened</param>
        /// <param name="reOpenEndDate">Date/time reopen phase was ended</param>
        /// <returns></returns>
        public static bool IsAbleToEditCritique(Nullable<DateTime> dateSubmitted, DateTime startDate, DateTime endDate, DateTime? reOpenStartDate, DateTime? reOpenEndDate)
        {
            //
            // The user (SRO/RTA) can edit a critique from the PanelManagement view if
            //      - the critique is not submitted
            //
            return (!IsCritiqueSubmitted(dateSubmitted) &&
            //
            // within the phase dates.  The phase dates will not overlap
            //
                    WithInDates(dateSubmitted, startDate, endDate, reOpenStartDate, reOpenEndDate));
        }
        /// <summary>
        /// Checks if the current date is within the dates specified.
        /// </summary>
        /// <param name="dateSubmitted">Date the content was submitted</param>
        /// <param name="startDate">Phase start date</param>
        /// <param name="endDate">Phase end date</param>
        /// <param name="reOpenStartDate">Re-open start date</param>
        /// <param name="reOpenEndDate">Re-open end date</param>
        /// <returns>True if the current date is within the date range; false otherwise</returns>
        public static bool WithInDates(Nullable<DateTime> dateSubmitted, DateTime startDate, DateTime endDate, DateTime? reOpenStartDate, DateTime? reOpenEndDate)
        {
            var currentTime = GlobalProperties.P2rmisDateTimeNow;
            //return ( (!dateSubmitted.HasValue) &&  ((currentTime >= startDate && currentTime < endDate) ||
            //    (reOpenStartDate != null && currentTime >= reOpenStartDate && reOpenEndDate != null && currentTime < reOpenEndDate)));
            return ((!dateSubmitted.HasValue) && (WithInDates(currentTime, startDate, endDate, reOpenStartDate, reOpenEndDate)) );

        }
        /// <summary>
        /// Checks if the current date is within the dates specified.
        /// </summary>
        /// <param name="timeToCheck">Date to test</param>
        /// <param name="startDate">Phase start date</param>
        /// <param name="endDate">Phase end date</param>
        /// <param name="reOpenStartDate">Re-open start date</param>
        /// <param name="reOpenEndDate">Re-open end date</param>
        /// <returns>True if the current date is within the date range; false otherwise</returns>
        public static bool WithInDates(DateTime timeToCheck, DateTime startDate, DateTime endDate, DateTime? reOpenStartDate, DateTime? reOpenEndDate)
        {
            return (
                    ((timeToCheck >= startDate && timeToCheck < endDate) ||
                    (reOpenStartDate != null && timeToCheck >= reOpenStartDate && reOpenEndDate != null && timeToCheck < reOpenEndDate))
                    );
        }
        /// <summary>
        /// Constructs a shortened version of a person's full name (first letter first name full name e.g. FLastname)
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <returns>Shortened full name</returns>
        public static string ConstructShortName(string firstName, string lastName)
        {
            string value = string.Empty;
            if (!String.IsNullOrWhiteSpace(firstName) && !String.IsNullOrWhiteSpace(lastName))
            {
                StringBuilder sb = new StringBuilder();
                value = sb.AppendFormat("{0}{1}", firstName.Substring(0, 1), lastName).ToString();
            }
            return value;
        }
        /// <summary>
        /// Formats a local DateTime value as a UTC string.  
        /// </summary>
        /// <param name="dateTime">DateTime value to format</param>
        /// <returns>Time formatted as UTC string</returns>
        /// <remarks>The standard UTC identifier ('Z') is not used</remarks>
        public static string FormatDateTimeAsUtc(DateTime? dateTime)
        {
            return (dateTime.HasValue) ? dateTime.Value.ToUniversalTime().ToString("MM/dd/yyyy hh:mm tt UTC") : string.Empty;
        }
        /// <summary>
        /// Formats the status with reason as a string
        /// </summary>
        /// <param name="status">The user account status</param>
        /// <param name="statusReason">the user account status reason</param>
        /// <returns></returns>
        public static string FormatStatus(string status, string statusReason)
        {
            string value = string.Empty;
            if (!string.IsNullOrWhiteSpace(status) && !string.IsNullOrWhiteSpace(statusReason))
            {
                StringBuilder sb = new StringBuilder();
                value = sb.AppendFormat("{0}{1}{2}", status, "-", statusReason).ToString();
            }
            return value;
        }
        /// <summary>
        /// Format signature
        /// </summary>
        /// <param name="name">The name signed</param>
        /// <param name="datetime">The date/time signed</param>
        /// <returns>The formatted signature in string format</returns>
        public static string FormatSignature(string name, DateTime? datetime)
        {
            return String.Format("{0} {1}", name, FormatEtDateTime(datetime));
        }
        /// <summary>
        /// Is reviewer profile type
        /// </summary>
        /// <param name="profileType">Profile type identifier</param>
        /// <returns>true if type is reviewer or prospect, false otherwise</returns>
        public static bool IsReviewer(int? profileType)
        {
            return profileType.HasValue && (profileType == 2 || profileType == 1);
        }
        /// <summary>
        /// Format reviewer name
        /// </summary>
        /// <param name="firstName">Reviewer first name</param>
        /// <param name="lastName">Reviewer last name</param>
        /// <param name="clientAssignmentTypeAbbreviation">Client assignment type abbreviation of the reviewer</param>
        /// <returns></returns>
        public static string FormatReviewerName(string firstName, string lastName, string clientAssignmentTypeAbbreviation) {
            return string.Format("{0}. {1} ({2})", firstName[0], lastName, clientAssignmentTypeAbbreviation);
        }
        /// <summary>
        /// Crop text to a format with ellipses
        /// </summary>
        /// <param name="text">Original text</param>
        /// <param name="length">Length before cropping</param>
        /// <returns>Text with ellipses</returns>
        public static string CropText(string text, int length)
        {
            if (text.Length > length)
            {
                text = text.Substring(0, length) + "...";
            }
            return text;
        }

        /// <summary>
        /// Concatenates the provided strings with comma if not null.
        /// </summary>
        /// <param name="string1">First string to left of comma.</param>
        /// <param name="string2">Second string to right of comma.</param>
        /// <returns>String concatenated with a comma seperator if not null</returns>
        public static string ConcatenateStringWithComma(string string1, string string2)
        {
            string result = string.Format("{0}{1}{2}", string1,
                (string1.IsNullOrEmpty() || string2.IsNullOrEmpty() ? string.Empty : ", "), string2);
            return result;
        }

        /// <summary>
        /// The score formatter not calculated with abstain.
        /// </summary>
        /// <param name="score">The score.</param>
        /// <param name="scoreType">Type of the score.</param>
        /// <param name="adjectival">The adjectival score equivalent.</param>
        /// <param name="isAbstained">whether the score has been abstained.</param>
        /// <param name="isCritiqueSubmitted">if set to <c>true</c> [critique is submitted].</param>
        /// <returns>
        /// Properly formatted score for conditions specified
        /// </returns>
        public static string ScoreFormatterNotCalculatedWithAbstain(decimal? score, string scoreType, string adjectival, bool isAbstained, bool isCritiqueSubmitted)
        {
            decimal theScore = score ?? 0;
            string formattedScore = String.Empty;
            if (isCritiqueSubmitted)
            {
                if (scoreType == string.Empty)
                {
                    formattedScore = Constants.ScoreNotApplicableMessage;
                }
                else if (isAbstained)
                {
                    formattedScore = Constants.Abstained;
                }
                else if (theScore > 0)
                {
                    if (String.Compare(scoreType, "Adjectival", StringComparison.InvariantCultureIgnoreCase) == 0)
                        formattedScore = adjectival;
                    else if (String.Compare(scoreType, "Decimal", StringComparison.InvariantCultureIgnoreCase) == 0)
                        formattedScore = FormatScoreDecimal(theScore);
                    else if (String.Compare(scoreType, "Integer", StringComparison.InvariantCultureIgnoreCase) == 0)
                        formattedScore = FormatScoreInteger(theScore);
                }
            }
            return formattedScore;
        }
        /// <summary>
        /// The score formatter not calculated with abstain.
        /// </summary>
        /// <param name="score">The score.</param>
        /// <param name="scoreType">Type of the score.</param>
        /// <param name="adjectival">The adjectival score equivalent.</param>
        /// <param name="isAbstained">whether the score has been abstained.</param>
        /// <returns>
        /// Properly formatted score for conditions specified
        /// </returns>
        public static string ScoreFormatterNotCalculatedWithAbstain(decimal? score, string scoreType, string adjectival, bool isAbstained)
        {
            return ScoreFormatterNotCalculatedWithAbstain(score, scoreType, adjectival, isAbstained, true);
        }
        /// <summary>
        /// Returns the empty string if the value is null.
        /// </summary>
        /// <param name="value">Value</param>
        /// <returns>Original value if not null; empty string otherwise</returns>
        public static string EmptyIfNull(string value)
        {
            return value ?? string.Empty;
        }
        /// <summary>
        /// Format an ApplicationWorkflowStep list entry
        /// </summary>
        /// <param name="number">Step number</param>
        /// <param name="stepName">Step name</param>
        /// <returns>Formatted list entry</returns>
        public static string WorkflowStepName(int number, string stepName)
        {
            return $"Phase {number} - {stepName}";
        }
        /// <summary>
        /// Returns the help desk email address
        /// </summary>
        public static string HelpDeskEmail
        {
            get
            {
                return ConfigManager.HelpDeskEmailAddress;
            }
        }
        /// <summary>
        /// Returns the help disk phone number
        /// </summary>
        public static string HelpDeskPhoneNumber
        {
            get
            {
                return ConfigManager.HelpDeskPhoneNumber;
            }
        }
        /// <summary>
        /// Returns the help disk hours
        /// </summary>
        public static string HelpDeskHours
        {
            get
            {
                return ConfigManager.HelpDeskHours;
            }
        }

        /// <summary>
        /// Constructs the site full site URL.
        /// </summary>
        /// <param name="urlScheme">The URL scheme.</param>
        /// <param name="urlHost">The URL host.</param>
        /// <returns>String representation of the web address</returns>
        public static string ConstructSiteUrl(string urlScheme, string urlHost)
        {
            return $"{urlScheme}{urlHost}";
        }

        /// <summary>
        /// Formats the discussion email date time.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public static string FormatDiscussionDateTime(DateTime? dateTime)
        {
            return (dateTime != null) ? dateTime.Value.ToString("dd-MMM-yy hh:mm tt ET") : string.Empty;
        }
        /// <summary>
        /// Adds business days to a specified date.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="days">The number of days to add.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">days cannot be negative;days</exception>
        public static DateTime AddBusinessDays(DateTime date, int days)
        {
            if (days < 0)
            {
                throw new ArgumentException("days cannot be negative", "days");
            }

            if (days == 0) return date;

            if (date.DayOfWeek == DayOfWeek.Saturday)
            {
                date = date.AddDays(2);
                days -= 1;
            }
            else if (date.DayOfWeek == DayOfWeek.Sunday)
            {
                date = date.AddDays(1);
                days -= 1;
            }

            date = date.AddDays(days / 5 * 7);
            int extraDays = days % 5;

            if ((int)date.DayOfWeek + extraDays > 5)
            {
                extraDays += 2;
            }

            return date.AddDays(extraDays);

        }
        /// <summary>
        /// Constructs the name of the backup file.
        /// </summary>
        /// <param name="stepName">Name of the step.</param>
        /// <param name="logNumber">The log number.</param>
        /// <param name="docExtension">The document extension.</param>
        /// <returns>full string file name</returns>
        public static string ConstructFileName(string stepName, string logNumber, string docExtension)
        {
            return $"{logNumber}_{stepName}.{docExtension}";
        }
        /// <summary>
        /// This is for Partial or Full in 
        /// </summary>
        /// <param name="restrictedAssignedFlag"></param>
        /// <returns></returns>
        public static string RestrictedFlag(bool restrictedAssignedFlag)
        {
            return (restrictedAssignedFlag) ? "Partial" : "Full";
        }

        /// <summary>
        /// Builds the hotel travel sublink.
        /// </summary>
        /// <param name="subTabLink">The sub tab link.</param>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <param name="sessionUserAssignmentId">The session user assignment identifier.</param>
        public static string BuildHotelTravelSublink(string subTabLink, int? panelUserAssignmentId, int? sessionUserAssignmentId)
        {
            string parameterName = (panelUserAssignmentId.HasValue) ? "panelUserAssignmentId" : "sessionUserAssignmentId";
            int parameterValue = (panelUserAssignmentId.HasValue) ? (int)panelUserAssignmentId : (int)sessionUserAssignmentId;

            return subTabLink + "?" + parameterName + "=" + parameterValue;
        }
    }
    
}
