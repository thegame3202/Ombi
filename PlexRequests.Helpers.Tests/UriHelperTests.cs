﻿#region Copyright
// /************************************************************************
//    Copyright (c) 2016 Jamie Rees
//    File: UriHelperTests.cs
//    Created By: Jamie Rees
//   
//    Permission is hereby granted, free of charge, to any person obtaining
//    a copy of this software and associated documentation files (the
//    "Software"), to deal in the Software without restriction, including
//    without limitation the rights to use, copy, modify, merge, publish,
//    distribute, sublicense, and/or sell copies of the Software, and to
//    permit persons to whom the Software is furnished to do so, subject to
//    the following conditions:
//   
//    The above copyright notice and this permission notice shall be
//    included in all copies or substantial portions of the Software.
//   
//    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
//    EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
//    MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
//    NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
//    LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
//    OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
//    WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//  ************************************************************************/
#endregion

using System;
using System.Collections.Generic;

using NUnit.Framework;

namespace PlexRequests.Helpers.Tests
{
    [TestFixture]
    public class UriHelperTests
    {
        [TestCaseSource(nameof(UriData))]
        public Uri CreateUri1(string uri)
        {
            return uri.ReturnUri();
        }

        [Test]
        public void CreateUriWithSsl()
        {
            var uri = "192.168.1.69";
            var result = uri.ReturnUri(8080, true);

            Assert.That(result, Is.EqualTo(new Uri("https://192.168.1.69:8080")));
        }

        [TestCaseSource(nameof(UriDataWithPort))]
        public Uri CreateUri2(string uri, int port)
        {
            return uri.ReturnUri(port);
        }

        [TestCaseSource(nameof(UriDataWithSubDir))]
        public Uri CreateUriWithSubDir(string uri, int port, bool ssl, string subDir)
        {
            return uri.ReturnUriWithSubDir(port, ssl, subDir);
        }

        private static IEnumerable<TestCaseData> UriData
        {
            get
            {
                yield return new TestCaseData("google.com").Returns(new Uri("http://google.com/"));
                yield return new TestCaseData("http://google.com").Returns(new Uri("http://google.com/"));
                yield return new TestCaseData("https://google.com").Returns(new Uri("https://google.com/"));
                yield return new TestCaseData("192.168.1.1").Returns(new Uri("http://192.168.1.1"));
                yield return new TestCaseData("0.0.0.0:5533").Returns(new Uri("http://0.0.0.0:5533"));
                yield return new TestCaseData("www.google.com").Returns(new Uri("http://www.google.com/"));
                yield return new TestCaseData("http://www.google.com/").Returns(new Uri("http://www.google.com/"));
                yield return new TestCaseData("https://www.google.com").Returns(new Uri("https://www.google.com/"));
                yield return new TestCaseData("www.google.com:443").Returns(new Uri("http://www.google.com:443/"));
                yield return new TestCaseData("https://www.google.com:443").Returns(new Uri("https://www.google.com/"));
                yield return new TestCaseData("http://www.google.com:443/id=2").Returns(new Uri("http://www.google.com:443/id=2"));
                yield return new TestCaseData("www.google.com:4438/id=22").Returns(new Uri("http://www.google.com:4438/id=22"));
            }
        }

        private static IEnumerable<TestCaseData> UriDataWithPort
        {
            get
            {
                yield return new TestCaseData("www.google.com", 80).Returns(new Uri("http://www.google.com:80/"));
                yield return new TestCaseData("www.google.com", 443).Returns(new Uri("http://www.google.com:443/"));
                yield return new TestCaseData("http://www.google.com", 443).Returns(new Uri("http://www.google.com:443/"));
                yield return new TestCaseData("https://www.google.com", 443).Returns(new Uri("https://www.google.com:443/"));
                yield return new TestCaseData("http://www.google.com/id=2", 443).Returns(new Uri("http://www.google.com:443/id=2"));
                yield return new TestCaseData("https://www.google.com/id=2", 443).Returns(new Uri("https://www.google.com:443/id=2"));
            }
        }

        private static IEnumerable<TestCaseData> UriDataWithSubDir
        {
            get
            {
                yield return new TestCaseData("www.google.com", 80, false, "test").Returns(new Uri("http://www.google.com:80/test"));
                yield return new TestCaseData("www.google.com", 443, false, "test").Returns(new Uri("http://www.google.com:443/test"));
                yield return new TestCaseData("http://www.google.com", 443, true, "test").Returns(new Uri("https://www.google.com:443/test"));
                yield return new TestCaseData("https://www.google.com", 443, true, "test").Returns(new Uri("https://www.google.com:443/test"));
            }
        }
    }
}