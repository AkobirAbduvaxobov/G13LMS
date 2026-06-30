# LMSPro — Project Status (Scan Report)

> Date: 2026-06-29
> Scope: `LMSPro.sln` (`LMSPro.Api` + `Chor`)

## Overview
LMSPro is an ASP.NET Core 8.0 Learning Management System API. It builds successfully today, but several entities only have DTOs/EF configs without Controllers, Services, Mappers or Validators. There is also some dead/commented code and one runtime bug. Overall completion ~60%.

## Architecture
- **Pattern:** Controller → Service (validation + cache) → Repository (`BaseRepository<T>` + specialized `CourseRepository`) → `AppDbContext`.
- **Mapping:** extension mappers (`ToEntity`, `ToGetDto`, `ToUpdateEntity`).
- **Cross-cutting:** FluentValidation, Serilog (File + MSSQL), MemoryCache + OutputCache, exception/request-logging middlewares.

## Entity relationships
```
Course (1)──(M) Enrollment (M)──(1) Student
Course (1)──(M) Lesson (1)──(M) Question
                  Lesson (1)──(M) Homework / Exam / Resource
Course (1)──(M) TeacherCourse (M)──(1) Teacher
```

## Completion matrix
| Entity | Controller | Service | Repository | DTOs | Mapper | Validator | FluentAPI |
|--------|:---------:|:------:|:----------:|:----:|:------:|:---------:|:---------:|
| Course | ✅ | ✅ | ✅ | ✅ | ⚠️ | ✅ | ✅ |
| Enrollment | ✅ | ✅ | ✅ | ✅ | ✅ | ❌ | ✅ |
| Lesson | ⚠️ | ⚠️ | ✅ | ✅ | ⚠️ | ✅ | ✅ |
| Question | ✅ | ✅ | ✅ | ✅ | ✅ | ❌ | ✅ |
| Exam | ❌ | ❌ | ✅ | ✅ | ✅ | ✅ | ✅ |
| Homework | ❌ | ❌ | ✅ | ✅ | ✅ | ⚠️ | ✅ |
| Resource | ❌ | ❌ | ✅ | ✅ | ❌ | ✅ | ✅ |
| Teacher | ❌ | ❌ | ✅ | ✅ | ❌ | ✅ | ✅ |
| TeacherCourse | ❌ | ❌ | ✅ | ✅ | ❌ | ❌ | ⚠️ |
| Student | ❌ | ❌ | ✅ | ⚠️ | ❌ | ❌ | ✅ |

✅ done · ⚠️ partial · ❌ missing

## Known issues
1. `LessonService.GetAllAsync()` throws `NotImplementedException` → `GET /api/lessons` returns 500.
2. `LessonsController` missing GetAll/GetById endpoints.
3. `StudentGetDto` is empty (no properties).
4. `CourseMapper` and `LessonMapper` missing `ToUpdateEntity`.
5. Missing validators: Question, Enrollment, Student, TeacherCourse, Homework Create.
6. DataSeeder + FilterConfigurations commented out in `Program.cs`.
7. Dead code: commented lines in `LessonMapper`, test comments in `AppDbContext`.
8. Wrong error text in `LessonService` (says "Course" not "Lesson").
9. Hardcoded DB credentials (`sa/1`) in connection string.
10. `Chor` project is unrelated (chain-of-responsibility demo) → candidate for removal.

## Build status
Builds with no compile errors. Runtime risks: Lesson GetAll 500, no seed data, hardcoded credentials.

See [PLAN.md](PLAN.md) for the ordered work items to finish the project.
