import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { ALL_COMPONENTS, ROOT_COMPONENT } from './components';
import { ALL_ROUTES } from './routes';
import { ALL_SERVICES } from './services';
import { ALL_RESOLVERS } from './resolvers';

@NgModule({
  declarations: [ALL_COMPONENTS],
  imports: [BrowserModule, HttpClientModule, ReactiveFormsModule, RouterModule.forRoot(ALL_ROUTES, { useHash: true })],
  providers: [...ALL_SERVICES, ...ALL_RESOLVERS],
  bootstrap: [ROOT_COMPONENT],
})
export class AppModule {}
