from mixpanel_api import Mixpanel
selector = '(behaviors["behavior_12"] > 2 and behaviors["behavior_13"] > 0)'
behaviors = r'[{"window": "30d", "name": "behavior_12", "event_selectors": [{"event": "watch_overview"}]}, {"window": "30d", "name": "behavior_13", "event_selectors": [{"event": "watch_meditation", "selector": "((event[\"$duration\"] > 299))"}]}]'
parameters = { 'selector' : selector, 'behaviors': behaviors }
mixpanel = Mixpanel('566129ca673e656d4f5bd91d149322ee', token='73167d0429d8da0c05c6707e832cbb46')
mixpanel.deduplicate_people(profiles=None, prop_to_match='$email', merge_props=True, case_sensitive=False, backup=True, backup_file='backup3')
mixpanel.export_people('people.csv', params=parameters, timezone_offset=-8, format='csv', compress=False)