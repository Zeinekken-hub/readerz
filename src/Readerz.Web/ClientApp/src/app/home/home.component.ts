import { Component, OnInit, ViewChild } from '@angular/core';
import { CardSetService } from '../services/card-set.service';
import { CardSet } from '../models/card-set';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { Router } from '@angular/router';
import { faPlay } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  public cardSets: MatTableDataSource<CardSet>
  public displayedColumns: string[] = ['name', 'like', 'timesPlayed', 'play']
  pageEvent: PageEvent;
  playIcon = faPlay;
  
  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(private cardSetService: CardSetService, public router: Router) {
  }

  ngOnInit(): void {
    let pageEvent = new PageEvent();
    pageEvent.pageIndex = 0;
    pageEvent.pageSize = 10;
    this.getData(pageEvent);
  }

  getData(pageEvent: PageEvent) {
    this.cardSetService.getCards(pageEvent.pageIndex.toString(), pageEvent.pageSize.toString()).subscribe(result => {
      this.paginator.length = result.totalPages;
      this.paginator.pageIndex = result.pageIndex;
      this.paginator.pageSize = result.pageSize;
      this.cardSets = new MatTableDataSource<CardSet>(result.data);
    }, err => console.log(err));
  }
}
