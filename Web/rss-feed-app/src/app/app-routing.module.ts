import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { FeedListComponent } from './feeds/feed-list.component';
import { FeedDetailComponent } from './feeds/feed-detail.component';
import { AppComponent } from './app.component';

const feedRoutes: Routes = [
  {path: 'feeds', component: FeedListComponent , children: [
    {path: ':id', component: FeedDetailComponent},
  ]}
];

@NgModule({
  imports: [RouterModule.forRoot(feedRoutes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
