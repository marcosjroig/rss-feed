import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { Feed } from './feeds.model';
import { FeedService } from './feed.service';

@Component({
  selector: 'app-feed-detail',
  templateUrl: './feed-detail.component.html'
})
export class FeedDetailComponent implements OnInit {
  id: number;
  feed: Feed;

  constructor(private route: ActivatedRoute, private feedService: FeedService) { }

  ngOnInit() {
     this.id = +this.route.snapshot.params['id'];
     this.feed = this.feedService.getFeed(this.id);

    this.route.params.subscribe(
      (params: Params)  => {
        this.feed = this.feedService.getFeed(+params['id']);
      }
    );
  }
}
