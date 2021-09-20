import { AdminService } from './../../_services/admin.service';
import { User } from './../../_models/user';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-admin-panel',
  templateUrl: './admin-panel.component.html',
  styleUrls: ['./admin-panel.component.css']
})
export class AdminPanelComponent implements OnInit {
  

  constructor() { }

  ngOnInit(): void {
  }

}
