import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Feed } from './feeds.model';
import { map } from 'rxjs/operators';

const API_URL = 'http://localhost:10140/api/RssFeed/';


@Injectable()
export class FeedService {

  private feeds: Feed[];
  constructor(private http: HttpClient) { }

  getFeeds(): Observable<Feed[]> {
    return this.http.get<Feed[]>(API_URL)
    .pipe(map(
      (feeds) => {
          this.feeds = feeds;
          return feeds;
       }
    ));
  }

  getFeed(index: number) {
    return this.feeds[index];
  }
}
