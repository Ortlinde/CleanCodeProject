namespace CleanCodeProject.C03
{
    public class HtmlUtil
    {
        // 可測試的HTML?
        public static string TestableHtml(PageData pageData, bool includeSuiteSetup)
        {
            WikiPage wikiPage = pageData.WikiPage;
            StringBuilder buffer = new StringBuilder();

            if (pageData.HasAttribute("Test"))
            {
                if (includeSuiteSetup)
                {
                    WikiPage suiteSetup =
                        PageCrawlerImpl.GetInheritedPage(
                            SuiteResponder.SUITE_SETUP_NAME, wikiPage
                        );
                    if (suiteSetup != null)
                    {
                        WikiPagePath pagePath =
                            suiteSetup.PageCrawler.GetFullPath(suiteSetup);
                        string pagePathName = PathParser.Render(pagePath);
                        buffer.Append("!include -setup .")
                            .Append(pagePathName)
                            .Append("\n");
                    }
                }

                WikiPage setup =
                    PageCrawlerImpl.GetInheritedPage("SetUp", wikiPage);
                if (setup != null)
                {
                    WikiPagePath setupPath =
                        wikiPage.PageCrawler.GetFullPath(setup);
                    string setupPathName = PathParser.Render(setupPath);
                    buffer.Append("!include -setup .")
                        .Append(setupPathName)
                        .Append("\n");
                }
            }

            buffer.Append(pageData.Content);

            if (pageData.HasAttribute("Test"))
            {
                WikiPage teardown =
                    PageCrawlerImpl.GetInheritedPage("TearDown", wikiPage);
                if (teardown != null)
                {
                    WikiPagePath tearDownPath =
                        wikiPage.PageCrawler.GetFullPath(teardown);
                    string tearDownPathName = PathParser.Render(tearDownPath);
                    buffer.Append("\n")
                        .Append("!include -teardown .")
                        .Append(tearDownPathName)
                        .Append("\n");
                }

                if (includeSuiteSetup)
                {
                    WikiPage suiteTeardown =
                        PageCrawlerImpl.GetInheritedPage(
                            SuiteResponder.SUITE_TEARDOWN_NAME,
                            wikiPage
                        );
                    if (suiteTeardown != null)
                    {
                        WikiPagePath pagePath =
                            suiteTeardown.PageCrawler.GetFullPath(suiteTeardown);
                        string pagePathName = PathParser.Render(pagePath);
                        buffer.Append("!include -teardown .")
                            .Append(pagePathName)
                            .Append("\n");
                    }
                }
            }

            pageData.Content = buffer.ToString();
            return pageData.Html;
        }
    }
}