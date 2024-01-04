import 'package:flutter/material.dart';
import 'package:studenda_mobile/model/schedule/subject.dart';
import 'package:studenda_mobile/widgets/schedule/schedule_item_widget.dart';

class DayScheduleWidget extends StatelessWidget {
  final List<Subject> subjects;

  const DayScheduleWidget({
    required this.subjects,
  });

  @override
  Widget build(BuildContext context) {
    return Container(
      decoration: BoxDecoration(
        border: Border.all(
          color: Colors.white,
        ),
        color: Colors.white,
        borderRadius: const BorderRadius.all(Radius.circular(5)),
      ),
    
      child: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        mainAxisSize: MainAxisSize.min,
        children: subjects.map((element) => ScheduleItemWidget(subject: element)).toList(),
      ),
    );
  }
}
