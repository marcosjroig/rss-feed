export class Feed {
    public link: string;
    public title: string;
    public content: string;
    public publishDate: Date;

    constructor(link: string, title: string, content: string, publishDate: string) {
        this.link = link;
        this.title = title;
        this.content = content;
        this.publishDate = new Date(Date.parse(publishDate));
    }
}
