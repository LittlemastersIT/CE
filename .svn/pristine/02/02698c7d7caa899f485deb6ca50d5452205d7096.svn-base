// update the album tab and sync things up
var currentTabIndex = 0;
function setArticleTab(tabId, articleId, i) {
    currentTabIndex = i;
    var tabs = $(tabId + ' .tab-articles-item');
    tabs.removeClass('show').addClass('hide');
    tabs.eq(i).removeClass('hide').addClass('show');

    var articles = $(articleId + ' ul li');
    articles.removeClass('selected');
    articles.eq(i).addClass('selected')
}
