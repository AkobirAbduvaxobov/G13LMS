# LMSPro — Work Plan to Finish

Ordered, checkable work items. Group A first (fixes), then B (cleanup/removal), then C (missing features), then D (final). Each item has a clear definition of done.

---

## Phase A — Fix existing bugs (do first)
- [x] **A1.** Implement `LessonService.GetAllAsync()` (remove `NotImplementedException`). Return mapped lessons, support pagination via `PaginatedLessonDto`.
- [x] **A2.** Add `GetAll` and `GetById` endpoints to `LessonsController`.
- [x] **A3.** Fix wrong error text in `LessonService` ("Course" → "Lesson").
- [x] **A4.** Add `ToUpdateEntity` mapper for Course and Lesson (so updates use mapper, not manual).
- [x] **A5.** Fill in `StudentGetDto` properties (Id, FirstName, LastName, Email, RegisteredAt).
- [ ] **A6.** Move DB credentials out of source (use User Secrets / env vars; placeholder in `appsettings.json`). _Left as-is to avoid breaking local DB._

## Phase B — Remove unused / incorrect code
- [x] **B1.** Removed `FilterConfigurations` + `CustomExceptionFilter` (dead; middlewares handle exceptions).
- [x] **B2.** Removed `LoggingActionFilter` (RequestLoggingMiddleware covers it).
- [x] **B3.** Deleted commented test code in `AppDbContext`.
- [x] **B4.** Removed commented-out mapping lines in `LessonMapper` (Resources mapping wired up).
- [ ] **B5.** Remove `Chor` project from solution (unrelated to LMS) OR confirm keeping it intentionally. _Awaiting confirmation._
- [ ] **B6.** Clean `LMSPro.Api.http` placeholder; add real request samples (optional).

## Phase C — Build missing features (Service + Mapper + Validator + Controller + DI per entity)
- [x] **C1. Teacher** — TeacherMapper, TeacherService(+I), TeachersController CRUD, register DI.
- [x] **C2. TeacherCourse** — Mapper, Service, Controller, Validator, FluentAPI config.
- [x] **C3. Exam** — ExamService(+I), ExamsController CRUD, register DI.
- [x] **C4. Homework** — HomeworkService(+I), HomeworkController CRUD, added `HomeworkCreateDtoValidator`, register DI.
- [x] **C5. Resource** — ResourceMapper, ResourceService(+I), ResourcesController CRUD, register DI.
- [x] **C6. Student** — StudentMapper, Create/Update DTOs, StudentService(+I), StudentsController CRUD, validators, register DI.
- [x] **C7.** Added validators: QuestionCreateDto, Enrollment Create/Update.

## Phase D — Wire-up & finalize
- [x] **D1.** Re-enabled DataSeeder in `Program.cs`.
- [x] **D2.** Registered all new services in `DependicyInjectionConfigurations`.
- [x] **D3.** Added CORS config.
- [x] **D4.** No entity changes → existing migration still valid.
- [ ] **D5.** Build + run; smoke-test every controller (Swagger). _Needs running SQL Server._
- [x] **D6.** Final review: no NotImplemented, no dead code, builds clean (0 errors).

---

### Order of execution
1. Phase A (A1→A6) — make it run correctly.
2. Phase B (B1→B6) — remove incorrect/unused code.
3. Phase C (C1→C7) — fill missing entities.
4. Phase D (D1→D6) — integrate, migrate, test.

### Definition of "done"
All entities have full CRUD, no `NotImplementedException`, no dead code, validators on all create/update DTOs, migration applied, builds clean, all endpoints verified in Swagger.
