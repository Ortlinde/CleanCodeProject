namespace CleanCodeProject.C09;

using NUnit.Framework;

/// <summary>
/// 測試驅動開發示例類
/// 用於測試Wiki頁面的XML層次結構和數據展示
/// </summary>
public class TestsDrivenDev
{
    private WikiPage root;
    private WikiPage crawler;
    private PageRequest request;
    private SimpleResponse response;

    /// <summary>
    /// 在每個測試方法執行前進行初始化
    /// </summary>
    [SetUp]
    public void Setup()
    {
        root = InMemoryPage.MakeRoot("root");
        crawler = new PageCrawlerImpl();
        request = new PageRequest();
    }

    /// <summary>
    /// 測試響應內容類型是否為XML
    /// </summary>
    [Test]
    public void ResponseShouldBeXml()
    {
        MakePages("PageOne");
        SubmitRequest("root", "pages");
        AssertResponseIsXml();
    }

    /// <summary>
    /// 測試頁面層次結構中包含PageOne
    /// </summary>
    [Test]
    public void PageHierarchyShouldContainPageOne()
    {
        MakePages("PageOne", "PageOne.ChildOne", "PageTwo");
        SubmitRequest("root", "pages");
        AssertResponseContains("<name>PageOne</name>");
    }

    /// <summary>
    /// 測試頁面層次結構中包含PageTwo
    /// </summary>
    [Test]
    public void PageHierarchyShouldContainPageTwo()
    {
        MakePages("PageOne", "PageOne.ChildOne", "PageTwo");
        SubmitRequest("root", "pages");
        AssertResponseContains("<name>PageTwo</name>");
    }

    /// <summary>
    /// 測試頁面層次結構中包含ChildOne
    /// </summary>
    [Test]
    public void PageHierarchyShouldContainChildOne()
    {
        MakePages("PageOne", "PageOne.ChildOne", "PageTwo");
        SubmitRequest("root", "pages");
        AssertResponseContains("<name>ChildOne</name>");
    }

    /// <summary>
    /// 測試符號鏈接不應出現在頁面層次結構中
    /// </summary>
    [Test]
    public void SymbolicLinksShouldNotAppearInPageHierarchy()
    {
        WikiPage page = MakePage("PageOne");
        MakePages("PageOne.ChildOne", "PageTwo");
        AddSymbolicLink(page, "SymPage", "PageTwo");
        SubmitRequest("root", "pages");
        AssertResponseDoesNotContain("SymPage");
    }

    /// <summary>
    /// 測試頁面數據包含指定內容
    /// </summary>
    [Test]
    public void PageDataShouldContainSpecifiedContent()
    {
        MakePageWithContent("TestPageOne", "test page");
        SubmitRequest("TestPageOne", "data");
        AssertResponseContains("test page");
    }

    /// <summary>
    /// 測試頁面數據應包含Test標籤
    /// </summary>
    [Test]
    public void PageDataShouldContainTestTag()
    {
        MakePageWithContent("TestPageOne", "test page");
        SubmitRequest("TestPageOne", "data");
        AssertResponseContains("<Test");
    }

    #region 輔助方法

    /// <summary>
    /// 創建多個Wiki頁面
    /// </summary>
    /// <param name="paths">頁面路徑數組</param>
    private void MakePages(params string[] paths)
    {
        foreach (string path in paths)
            crawler.AddPage(root, PathParser.Parse(path));
    }

    /// <summary>
    /// 創建單個Wiki頁面
    /// </summary>
    /// <param name="path">頁面路徑</param>
    /// <returns>創建的Wiki頁面</returns>
    private WikiPage MakePage(string path)
    {
        return crawler.AddPage(root, PathParser.Parse(path));
    }

    /// <summary>
    /// 創建帶有內容的Wiki頁面
    /// </summary>
    /// <param name="path">頁面路徑</param>
    /// <param name="content">頁面內容</param>
    private void MakePageWithContent(string path, string content)
    {
        crawler.AddPage(root, PathParser.Parse(path), content);
    }

    /// <summary>
    /// 添加符號鏈接到指定頁面
    /// </summary>
    /// <param name="page">目標頁面</param>
    /// <param name="name">鏈接名稱</param>
    /// <param name="target">鏈接目標</param>
    private void AddSymbolicLink(WikiPage page, string name, string target)
    {
        PageData data = page.GetData();
        WikiPageProperties props = data.GetProperties();
        WikiPageProperty symLinks = props.Set(SymbolicPage.PROPERTY_NAME);
        symLinks.Set(name, target);
        page.Commit(data);
    }

    /// <summary>
    /// 提交頁面請求
    /// </summary>
    /// <param name="resource">資源名稱</param>
    /// <param name="type">請求類型</param>
    private void SubmitRequest(string resource, string type)
    {
        request.SetResource(resource);
        request.AddInput("type", type);
        response = GetResponse();
    }

    /// <summary>
    /// 獲取響應
    /// </summary>
    /// <returns>SimpleResponse類型的響應</returns>
    private SimpleResponse GetResponse()
    {
        return (SimpleResponse)new SerializedPageResponder()
            .MakeResponse(new FitNesseContext(root), request);
    }

    /// <summary>
    /// 驗證響應是否為XML格式
    /// </summary>
    private void AssertResponseIsXml()
    {
        Assert.That(response.GetContentType(), Is.EqualTo("text/xml"));
    }

    /// <summary>
    /// 驗證響應是否包含指定的值
    /// </summary>
    /// <param name="expectedValues">期望包含的值</param>
    private void AssertResponseContains(params string[] expectedValues)
    {
        string content = response.GetContent();
        foreach (string value in expectedValues)
            Assert.That(content, Does.Contain(value));
    }

    /// <summary>
    /// 驗證響應是否不包含指定的值
    /// </summary>
    /// <param name="value">期望不包含的值</param>
    private void AssertResponseDoesNotContain(string value)
    {
        Assert.That(response.GetContent(), Does.Not.Contain(value));
    }

    #endregion
}
