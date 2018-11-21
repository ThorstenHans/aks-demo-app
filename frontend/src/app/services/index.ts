import { SessionsService } from './sessions.service';
import { SessionsServiceRef } from './ref/sessions.service.ref';
import { VotingsService } from './votings.service';
import { VotingsServiceRef } from './ref/votings.service.ref';
import { ShareService } from './share.service';
import { ShareServiceRef } from './ref/share.service.ref';
import { ExportService } from './export.service';
import { ExportServiceRef } from './ref/export.service.ref';

export const ALL_SERVICES = [
  { provide: SessionsService, useClass: SessionsServiceRef },
  { provide: VotingsService, useClass: VotingsServiceRef },
  { provide: ShareService, useClass: ShareServiceRef },
  { provide: ExportService, useClass: ExportServiceRef }
];
