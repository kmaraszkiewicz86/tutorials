import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { of } from 'rxjs'
import $ from "jquery";

import { HomeComponent } from './home.component';
import { UsersService } from '../services/users/users.service'

describe('HomeComponent', () => {
  let component: HomeComponent;
  let fixture: ComponentFixture<HomeComponent>;
  let userService: UsersService;

  beforeEach(() => {

    TestBed.configureTestingModule({
      declarations: [ HomeComponent ],
      providers: [ UsersService ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HomeComponent);
    component = fixture.componentInstance;

    userService = TestBed.get(UsersService);
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  describe('users', () => {
    it('should generate users list', () => {

      let users = [{
        id: '1',
        name: 'Jane',
        role: 'Designer',
        pokemon: 'Blastoise'
      }]

      let element = $('#users').find('.role').each(function(i){
        expect(this.textContent).toEqual(`Role: ` + users[0].role)
      })

      spyOn(userService, 'all').and.returnValue(of(users));

      component.ngOnInit()

      expect(component.users).toEqual(users);
    })
  })


});
