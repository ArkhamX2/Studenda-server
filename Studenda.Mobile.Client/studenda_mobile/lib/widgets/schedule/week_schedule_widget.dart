import 'package:flutter/material.dart';
import 'package:studenda_mobile/model/schedule/Management/day_schedule.dart';
import 'package:studenda_mobile/widgets/schedule/day_schedule_widget.dart';
import 'package:studenda_mobile/widgets/schedule/position_values.dart';

class WeekScheduleWidget extends StatelessWidget {
  final List<DaySchedule> schedule;
  final List<GlobalKey<State<StatefulWidget>>> keys;
  const WeekScheduleWidget({
    super.key,
    required this.schedule,
    required this.keys,
  });

  @override
  Widget build(BuildContext context) {
    return Column(
      children: schedule
          .asMap()
          .map(
            (key, element) => MapEntry(
              key,
              DayScheduleWidget(
                key: keys[key],
                dayTitle: weekPositionFullValues[element.weekPosition],
                subjects: element.subjects,
                isTitleRequired: true,
              ),
            ),
          )
          .values
          .toList(),
    );
  }
}
