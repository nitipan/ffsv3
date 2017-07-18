import { Ffs.WebPage } from './app.po';

describe('ffs.web App', () => {
  let page: Ffs.WebPage;

  beforeEach(() => {
    page = new Ffs.WebPage();
  });

  it('should display welcome message', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('Welcome to app!');
  });
});
