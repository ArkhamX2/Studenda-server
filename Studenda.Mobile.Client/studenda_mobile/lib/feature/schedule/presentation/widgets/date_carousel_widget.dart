
import 'package:flutter/material.dart';
import 'package:studenda_mobile/feature/schedule/presentation/widgets/position_values.dart';
import 'package:studenda_mobile/resources/colors.dart';

class DateCarouselWidget extends StatelessWidget {
  final List<String> dates;
  
  final Function(int) onDateTap;
  final Function() onPrevTap;
  final Function() onNextTap;

  const DateCarouselWidget({
    super.key, required this.dates, required this.onDateTap, required this.onPrevTap, required this.onNextTap,
  });

  @override
  Widget build(BuildContext context) {
    return Container(
      decoration: BoxDecoration(
        border: Border.all(
          color: Colors.white,
        ),
        color: const Color.fromARGB(255, 211, 201, 253),
        borderRadius: const BorderRadius.all(Radius.circular(5)),
      ),
      child: Row(
        mainAxisAlignment: MainAxisAlignment.center,
        mainAxisSize: MainAxisSize.min,
        children: [
          IconButton(
            icon: const Icon(
              Icons.chevron_left_outlined,
              color: Colors.white,
            ),
            onPressed: onPrevTap,
          ),
          Expanded(
            child: Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: dates
                  .asMap()
                  .map(
                    (key, value) => MapEntry(
                      key,
                      GestureDetector(
                        onTap: (){onDateTap(key);},
                        child: _DateCarouselItemWidget(
                          day: key,
                          date: value,
                        ),
                      ),
                    ),
                  )
                  .values
                  .toList(),
            ),
          ),
          IconButton(
            icon: const Icon(
              Icons.chevron_right_outlined,
              color: Colors.white,
            ),
            onPressed: onNextTap,
          ),
        ],
      ),
    );
  }
}

class _DateCarouselItemWidget extends StatelessWidget {
  final int day;
  final String date;

  const _DateCarouselItemWidget({
    required this.day,
    required this.date,
  });

  @override
  Widget build(BuildContext context) {
    return Column(
      children: [
        Text(
          date,
          style: const TextStyle(
            color: mainForegroundColor,
            fontSize: 30,
          ),
        ),
        Text(
          weekPositionValues[day],
          style: const TextStyle(
            color: mainForegroundColor,
            fontSize: 12,
            fontWeight: FontWeight.w400,
          ),
        ),
      ],
    );
  }
}
