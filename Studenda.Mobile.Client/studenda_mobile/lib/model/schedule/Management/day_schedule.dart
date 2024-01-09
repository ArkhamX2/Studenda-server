import 'package:studenda_mobile/feature/schedule/domain/entities/subject_entity.dart';

class DaySchedule {
  final int weekPosition;
  final List<SubjectEntity> subjects;
  DaySchedule(this.weekPosition,this.subjects);
}
