import { SessionsService } from './sessions.service';
import { SessionsServiceRef } from './ref/sessions.service.ref';
import { VotingsService } from './votings.service';
import { VotingsServiceRef } from './ref/votings.service.ref';
import { ShareService } from './share.service';
import { ShareServiceRef } from './ref/share.service.ref';
import { ExportService } from './export.service';
import { ExportServiceRef } from './ref/export.service.ref';
import { environment } from '../../environments/environment';
import { SessionsServiceMock } from './mocks/sessions.service.mock';
import { HttpClient } from '@angular/common/http';
import { ShareServiceMock } from './mocks/share.service.mock';
import { ExportServiceMock } from './mocks/export.service.mock';
import { VotingsServiceMock } from './mocks/votings.services.mock';

export function sessionsServiceFactory(httpClient: HttpClient): SessionsService {
  if (environment.name === 'Development') {
    return new SessionsServiceMock();
  }
  return new SessionsServiceRef(httpClient);
}

export function votingsServiceFactory(httpClient: HttpClient): VotingsService {
  if (environment.name === 'Development') {
    return new VotingsServiceMock();
  }
  return new VotingsServiceRef(httpClient);
}

export function exportServiceFactory(httpClient: HttpClient): ExportService {
  if (environment.name === 'Development') {
    return new ExportServiceMock();
  }
  return new ExportServiceRef(httpClient);
}

export function shareServiceFactory(httpClient: HttpClient): ShareService {
  if (environment.name === 'Development') {
    return new ShareServiceMock();
  }
  return new ShareServiceRef(httpClient);
}
export const ALL_SERVICES = [
  { provide: SessionsService, useFactory: sessionsServiceFactory, deps: [HttpClient] },
  { provide: VotingsService, useFactory: votingsServiceFactory, deps: [HttpClient] },
  { provide: ShareService, useFactory: shareServiceFactory, deps: [HttpClient] },
  { provide: ExportService, useFactory: exportServiceFactory, deps: [HttpClient] }
];
