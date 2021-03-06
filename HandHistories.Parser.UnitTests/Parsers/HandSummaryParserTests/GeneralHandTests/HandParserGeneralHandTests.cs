﻿using System;
using HandHistories.Objects.GameDescription;
using HandHistories.Parser.UnitTests.Parsers.Base;
using NUnit.Framework;

namespace HandHistories.Parser.UnitTests.Parsers.HandSummaryParserTests.GeneralHandTests
{
    [TestFixture("PartyPoker", 11614240016l, "4/3/2012 08:24:43", 6, 5, null, null, "GeneralHand")]
    [TestFixture("PokerStars", 78451486205l, "4/7/2012 06:58:27", 2, 6, 0.59, 13.12, "GeneralHand")]
    [TestFixture("PokerStars", 84414134468l, "8/7/2012 18:40:01", 1, 6, 0.01, 0.30, "ZoomHand")]
    [TestFixture("PokerStars", 84437728533l, "8/8/2012 07:56:44", 3, 9, 2.50, 62.63, "SidePot")]
    [TestFixture("Merge", 533636922070l, "4/17/2012 01:58:48", 8, 9, null, null, "GeneralHand")]
    [TestFixture("IPoker", 3251939984l, "4/17/2012 19:30:53", 9, 4, null, null, "GeneralHand")]
    [TestFixture("OnGame", 524409056039l, "5/28/2012 07:08:00", 10, 4, null, null, "GeneralHand")]
    [TestFixture("Pacific", 176497340, "5/30/2012 14:29:43", 2, 3, null, null, "GeneralHand")]
    [TestFixture("Entraction", 2645975604, "5/30/2012 13:49:35", 4, 2, null, null, "GeneralHand")]
    [TestFixture("FullTilt", 26862429938, "12/31/2010 21:07:17", 5, 5, 2.10, 42.90, "GeneralHand")]  
    class HandParserGeneralHandTests : HandHistoryParserBaseTests 
    {
        private readonly long _expectedHandId;
        private readonly int _expectedDealerButtonPosition;
        private readonly int _expectedNumberOfPlayers;
        private readonly string _handFile;
        private readonly decimal? _expectedRake;
        private readonly decimal? _expectedPotSize;
        private readonly DateTime _expectedDateTime;
        private readonly string _handText;

        public HandParserGeneralHandTests(string site, 
                                          long expectedHandId,
                                          string expectedDateOfHand,
                                          int expectedDealerButtonPosition,
                                          int expectedNumberOfPlayers,
                                          double? expectedRake,
                                          double? expectedPotSize,
                                          string handFile)
            : base(site)
        {
            _expectedHandId = expectedHandId;
            _expectedDealerButtonPosition = expectedDealerButtonPosition;
            _expectedNumberOfPlayers = expectedNumberOfPlayers;
            _handFile = handFile;
            if (expectedRake != null) _expectedRake = (decimal)expectedRake;
            if (expectedPotSize != null) _expectedPotSize = (decimal)expectedPotSize;

            try
            {              
                _expectedDateTime = DateTime.Parse(expectedDateOfHand);

                _handText = SampleHandHistoryRepository.GetGeneralHandHistoryText(PokerFormat.CashGame, Site, _handFile);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void ParseHandId_Works()
        {            
            Assert.AreEqual(_expectedHandId, GetSummmaryParser().ParseHandId(_handText), "IHandHistorySummaryParser: ParseHandId");
            Assert.AreEqual(_expectedHandId, GetParser().ParseHandId(_handText), "IHandHistoryParser: ParseHandId");
        }

        [Test]
        public void ParseExtraDetails_Works()
        {
            var summary = GetSummmaryParser().ParseFullHandSummary(_handText);

            Assert.AreEqual(_expectedRake, summary.Rake, "Rake");
            Assert.AreEqual(_expectedPotSize, summary.TotalPot, "TotalPot");
        }

        [Test]
        public void ParseDateOfHand_ConvertsToUtcDateTime()
        {
            switch (Site)
            {
                case SiteName.Unknown:
                    Assert.Ignore("Not implemented for site " + Site);
                    break;
            }

            Assert.AreEqual(_expectedDateTime, GetSummmaryParser().ParseDateUtc(_handText), "IHandHistorySummaryParser: ParseDateUtc");
            Assert.AreEqual(_expectedDateTime, GetParser().ParseDateUtc(_handText), "IHandHistoryParser: ParseDateUtc");
        }


        [Test]
        public void ParseDealerButtonPosition_Works()
        {
            switch (Site)
            {
                case SiteName.Unknown:
                    Assert.Ignore("Not implemented for site " + Site);
                    break;
            }

            Assert.AreEqual(_expectedDealerButtonPosition, GetSummmaryParser().ParseDealerPosition(_handText), "IHandHistorySummaryParser: ParseDealerPosition");
            Assert.AreEqual(_expectedDealerButtonPosition, GetParser().ParseDealerPosition(_handText), "IHandHistoryParser: ParseDealerPosition");
        }

        [Test]
        public void ParseNumPlayers_Works()
        {
            switch (Site)
            {
                case SiteName.Unknown:
                    Assert.Ignore("Not implemented for site " + Site);
                    break;
            }

            Assert.AreEqual(_expectedNumberOfPlayers, GetSummmaryParser().ParseNumPlayers(_handText), "IHandHistorySummaryParser: ParseNumPlayers");
            Assert.AreEqual(_expectedNumberOfPlayers, GetParser().ParseNumPlayers(_handText), "IHandHistoryParser: ParseNumPlayers");
        }
    }
}
