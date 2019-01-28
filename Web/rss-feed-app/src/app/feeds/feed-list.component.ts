import { Component, OnInit } from '@angular/core';
import { FeedService } from './feed.service';
import { Feed } from './feeds.model';

@Component({
  selector: 'app-feed-list',
  templateUrl: './feed-list.component.html'
})
export class FeedListComponent implements OnInit {
  feeds: Feed[];

  constructor(private feedService: FeedService) { }

  ngOnInit() {
    this.getFeeds();
  }

  getFeeds() {
    this.feedService.getFeeds()
    .subscribe(feeds => this.feeds = feeds);
  }
}
