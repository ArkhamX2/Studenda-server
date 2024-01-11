import 'package:flutter/material.dart';
import 'package:studenda_mobile/feature/schedule/domain/entities/subject_entity.dart';
import 'package:studenda_mobile/feature/schedule/presentation/widgets/schedule_item_widget.dart';
import 'package:studenda_mobile/resources/colors.dart';

class DayScheduleWidget extends StatelessWidget {
  final String dayTitle;
  final List<SubjectEntity> subjects;
  final bool isTitleRequired;

  const DayScheduleWidget({
    super.key,
    required this.dayTitle,
    required this.subjects,
    required this.isTitleRequired,
  });

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.symmetric(vertical: 16.0),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          _DayTitleWidget(dayTitle: dayTitle, isRequired: isTitleRequired),
          const SizedBox(height: 17,),
          Container(
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
          ),
        ],
      ),
    );
  }
}

class _DayTitleWidget extends StatelessWidget {
  const _DayTitleWidget({
    required this.dayTitle, required this.isRequired,
  });

  final String dayTitle;
  final bool isRequired;

  @override
  Widget build(BuildContext context) {
    return isRequired? Text(dayTitle,
    style: const TextStyle(
        color: mainForegroundColor,
        fontSize: 20,
        fontWeight: FontWeight.bold,
    ),) : Container();
  }
}
