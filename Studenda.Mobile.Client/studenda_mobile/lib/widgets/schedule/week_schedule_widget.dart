import 'package:flutter/material.dart';
import 'package:studenda_mobile/model/schedule/Management/day_schedule.dart';
import 'package:studenda_mobile/widgets/schedule/day_schedule_widget.dart';
import 'package:studenda_mobile/widgets/schedule/position_values.dart';

class WeekScheduleWidget extends StatelessWidget {
  final List<DaySchedule> schedule;

  const WeekScheduleWidget({super.key, required this.schedule});

  @override
  Widget build(BuildContext context) {
    return Column(
      children: schedule.map((element) => DayScheduleWidget(dayTitle: weekPositionFullValues[element.weekPosition], subjects: element.subjects, isTitleRequired: true)).toList(),
    );
  }
}
